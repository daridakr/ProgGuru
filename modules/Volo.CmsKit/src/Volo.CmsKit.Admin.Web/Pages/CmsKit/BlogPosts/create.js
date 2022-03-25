$(function () {
    var l = abp.localization.getResource("CmsKit");
    var $formCreate = $('#form-blog-post-create');
    var $selectBlog = $('#BlogSelectionSelect');
    var $selectUser = $('#UserSelectionSelect');
    var $title = $('#ViewModel_Title');
    var $shortDescription = $('#ViewModel_ShortDescription');
    var $url = $('#ViewModel_Slug');
    var $tagsWrapper = $('#blog-post-tags-wrapper');
    var isTagsEnabled = true;

    $formCreate.data('validator').settings.ignore = ":hidden, [contenteditable='true']:not([name]), .tui-popup-wrapper";

    $('#submitButton').click(function (e) {
        e.preventDefault();
        if ($formCreate.valid()) {
            $formCreate.ajaxSubmit({
                success: function (result) {
                    if (isTagsEnabled) {
                        submitEntityTags(result.id);
                    }
                    else {
                        finishSaving();
                    }
                },
                error: function (result) {
                    abp.notify.error(result.responseJSON.error.message);
                    abp.ui.clearBusy();
                }
            });
        }
        else {
            abp.ui.clearBusy();
        }
    });

    function initSelectBlog() {
        $selectBlog.data('autocompleteApiUrl', '/api/app/group');
        $selectBlog.data('autocompleteDisplayProperty', 'title');
        $selectBlog.data('autocompleteValueProperty', 'id');
        $selectBlog.data('autocompleteItemsProperty', 'items');
        $selectBlog.data('autocompleteFilterParamName', 'filter');

        abp.dom.initializers.initializeAutocompleteSelects($selectBlog);
    }

    function initSelectUser() {
        $selectUser.data('autocompleteApiUrl', '/api/identity/users');
        $selectUser.data('autocompleteDisplayProperty', 'userName');
        $selectUser.data('autocompleteValueProperty', 'id');
        $selectUser.data('autocompleteItemsProperty', 'items');
        $selectUser.data('autocompleteFilterParamName', 'filter');

        abp.dom.initializers.initializeAutocompleteSelects($selectUser);
    }

    initSelectBlog();
    initSelectUser();

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
            }).then(function (result) {
                finishSaving(result);
            });
    }

    function getUppyHeaders() {
        var headers = {};
        headers[abp.security.antiForgery.tokenHeaderName] = abp.security.antiForgery.getToken();

        return headers;
    }

    function finishSaving() {
        abp.notify.success(l('SuccessfullySaved'));
        abp.ui.clearBusy();
        location.href = "/";
    }

    var urlEdited = false;
    var reflectedChange = false;

    $url.on('change', function () {
        reflectUrlChanges();
    });

    $title.on('keyup paste', function () {
        reflectUrlChanges();
    });

    function reflectUrlChanges() {
        var title = $title.val().toLocaleLowerCase();

        if (urlEdited) {
            title = $url.val();
        }

        var slugified = slugify(title, {
            lower: true
        });

        if (slugified != $url.val()) {
            reflectedChange = true;

            $url.val(slugified);

            reflectedChange = false;
        }
    }

    $url.change(function () {
        if (!reflectedChange) {
            urlEdited = true;
        }
    });

    var shorDescriptionEdited = false;

    function reflectContentChanges(htmlContent) {
        if (shorDescriptionEdited) {
            return;
        }

        var plainValue = jQuery('<div>').html(htmlContent).text().replace(/\n/g, ' ').substring(0, 120);

        $shortDescription.val(plainValue);
    }

    $shortDescription.on('change', function () {
        shorDescriptionEdited = true;
    });

    $selectBlog.on('change', function () {
        var blogId = $selectBlog.val();
        volo.cmsKit.blogs.blogFeature
            .getOrDefault(blogId, 'CmsKit.Tags')
            .then(function (result) {
                if (result) {
                    isTagsEnabled = result.isEnabled == true
                    if (isTagsEnabled === true) {
                        $tagsWrapper.removeClass('d-none');
                    }
                    else {
                        $tagsWrapper.addClass('d-none');
                    }
                }
            });
    });

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
                    reflectContentChanges(editor.getHtml());
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
        var file = this.files; //Files[0] = 1st file
        if (file[0])
            $('.preview').attr('src', URL.createObjectURL(file[0]));
    });
});