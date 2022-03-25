$(function () {
    var l = abp.localization.getResource("CmsKit");

    var $formUpdate = $('#form-blog-post-update');
    var $slug = $('#ViewModel_Slug');
    var $blogPostIdInput = $('#Id');
    var isTagsEnabled = true;

    $formUpdate.data('validator').settings.ignore = ":hidden, [contenteditable='true']:not([name]), .tui-popup-wrapper";

    $('#submitButton').click(function (e) {
        e.preventDefault();
        if ($formUpdate.valid()) {
            abp.ui.setBusy();
            $formUpdate.ajaxSubmit({
                success: function (result) {
                    if (isTagsEnabled) {
                        submitEntityTags($blogPostIdInput.val());
                    }
                    else {
                        finishSaving(result);
                    }
                },
                error: function (result) {
                    abp.ui.clearBusy();
                    abp.notify.error(result.responseJSON.error.message);
                }
            });
        }
        else {
            abp.ui.clearBusy();
        }
    });

    function submitEntityTags(blogPostId) {
        var tagsValue = $('#tagEditor').val();
        var tags = tagsValue.split(',').map(x => x.trim()).filter(x => x);

        if (tags.length === 0) {
            finishSaving();
            return;
        }

        volo.cmsKit.admin.tags.entityTagAdmin
            .setEntityTags({
                entityType: 'BlogPost',
                entityId: blogPostId,
                tags: tags
            })
            .then(function (result) {
                finishSaving(result);
            });
    }

    function getUppyHeaders() {
        var headers = {};
        headers[abp.security.antiForgery.tokenHeaderName] = abp.security.antiForgery.getToken();

        return headers;
    }

    function finishSaving(result) {
        abp.notify.success(l('SuccessfullySaved'));
        abp.ui.clearBusy();
        location.href = "/";
    }

    $slug.on('change', function () {
        reflectUrlChanges();
    });

    function reflectUrlChanges() {

        var title = $slug.val();

        var slugified = slugify(title);

        if (slugified != title) {
            $slug.val(slugified, {
                lower: true
            });
        }
    }

    // -----------------------------------
    var fileUploadUri = "/api/cms-kit-admin/media/blogpost";
    var fileUriPrefix = "/api/cms-kit/media/";

    initAllEditors();

    function initAllEditors() {
        $('.content-editor').each(function (i, item) {
            initEditor(item);
        });
    }

    function initEditor(element) {
        var $editorContainer = $(element);
        var inputName = $editorContainer.data('input-id');
        var $editorInput = $('#' + inputName);
        var initialValue = $editorInput.val();

        var editor = new toastui.Editor({
            el: $editorContainer[0],
            usageStatistics: false,
            useCommandShortcut: true,
            initialValue: initialValue,
            previewStyle: 'tab',
            plugins: [toastui.Editor.plugin.codeSyntaxHighlight],
            height: "100%",
            minHeight: "25em",
            initialEditType: 'markdown',
            language: $editorContainer.data("language"),
            hooks: {
                addImageBlobHook: uploadFile,
            },
            events: {
                change: function (_val) {
                    $editorInput.val(editor.getMarkdown());
                    $editorInput.trigger("change");
                }
            }
        });
    }

    function uploadFile(blob, callback, source) {
        var UPPY_OPTIONS = {
            endpoint: fileUploadUri,
            formData: true,
            fieldName: "file",
            method: "post",
            headers: getUppyHeaders()
        };

        var UPPY = Uppy.Core().use(Uppy.XHRUpload, UPPY_OPTIONS);

        UPPY.reset();

        UPPY.addFile({
            id: "content-file",
            name: blob.name,
            type: blob.type,
            data: blob,
        });

        UPPY.upload().then((result) => {
            if (result.failed.length > 0) {
                abp.message.error("File upload failed");
            } else {
                var mediaDto = result.successful[0].response.body;
                var fileUrl = (fileUriPrefix + mediaDto.id);

                callback(fileUrl, mediaDto.name);
            }
        });
    }

    $('input[type="file"]').change(function () {
        var file = this.files;
        if (file[0])
            $('.preview').attr('src', URL.createObjectURL(file[0]));
    });

});
