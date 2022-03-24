$(document).ready(function () {
    var l = abp.localization.getResource('ProgGuru');

    var createProjectModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var editProjectModal = new abp.ModalManager(abp.appPath + 'Projects/EditModal');

    var createCommonSkillModal = new abp.ModalManager(abp.appPath + 'Users/Skills/CreateCommonSkillModal');
    var createLanguageSkillModal = new abp.ModalManager(abp.appPath + 'Users/Skills/CreateLanguageSkillModal');
    var editLanguageSkillModal = new abp.ModalManager(abp.appPath + 'Users/Skills/EditLanguageSkillModal');
    var createProfSkillModal = new abp.ModalManager(abp.appPath + 'Users/Skills/CreateProfSkillModal');
    var editProfSkillModal = new abp.ModalManager(abp.appPath + 'Users/Skills/EditProfSkillModal');

    var createJobModal = new abp.ModalManager(abp.appPath + 'Users/JobExperiences/CreateJobExperienceModal');
    var editJobModal = new abp.ModalManager(abp.appPath + 'Users/JobExperiences/EditJobExperienceModal');

    var createCourseModal = new abp.ModalManager(abp.appPath + 'Users/Courses/CreateCourseModal');
    var editCourseModal = new abp.ModalManager(abp.appPath + 'Users/Courses/EditCourseModal');

    // CREATE DATATABLE
    var dataTable = $('table#ProjectsTable').DataTable({
        "language": {
            lengthMenu: l('lengthMenu'),
            zeroRecords: l('zeroRecords'),
            info: l('info'),
            search: l('DataSearch'),
            paginate: {
                first: l('first'),
                last: l('last'),
                next: l('next'),
                previous: l('previous')
            }
        }
    });

    // PRINT TABLE
    const tableToPrint = document.getElementById('ProjectsTable');

    function printData(tableToPrint) {
        Popup($(tableToPrint).html());
    }

    function Popup(data) {
        const mywindow = window.open('', 'ProjectsTable', 'height=600, width=1000');
        mywindow.document.write('<link rel="stylesheet" href="styles.css" type="text/css" />');
        mywindow.document.write(tableToPrint.outerHTML);
        mywindow.document.close(); // для IE >= 10 
        mywindow.focus();          // для IE >= 10 
        mywindow.print();
        mywindow.close();
        return true;
    }

    $(document).on('click', '#printTable', function () {
        printData();
        return false;
    });

    // COPY TABLE
    (function () {
        new Clipboard('#copy-button');
    })();

    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
            document.getElementById('upload').submit();
        }
    }

    $(".file-upload").on('change', function () {
        readURL(this);
    });

    $(".upload-button").on('click', function () {
        $(".file-upload").click();
    });

    // CREATE NEW PROJECT
    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createProjectModal.open();
    });

    createProjectModal.onResult(function () {
        location.reload();
    });

    // EDIT PROJECT
    $(document).on('click', '#UpdateProjectButton', function () {
        editProjectModal.open({ id: this.value });
    });

    editProjectModal.onResult(function () {
        location.reload();
    });

    // DELETE PROJECT
    $(document).on('click', '#DeleteProjectButton', function () {
        if (window.confirm(l('ProjectDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.projects.project
                .delete(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // EDIT ARICLE
    $(document).on('click', '#UpdateArticleButton', function () {
        location.href = "/Cms/BlogPosts/Update/" + this.value
    });

    // DELETE ARICLE
    $(document).on('click', '#DeleteArticleButton', function () {
        if (window.confirm(l('ArticleDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.projects.project
                .deleteArticle(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // CREATE COMMON SKILL
    $('#NewCommonSkillButton').click(function (e) {
        e.preventDefault();
        createCommonSkillModal.open();
    });

    createCommonSkillModal.onResult(function () {
        location.reload();
    });

    // DELETE COMMON SKILL
    $(document).on('click', '#DeleteCommonSkillButton', function () {
        if (window.confirm(l('SkillDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.users.skills.commonSkill
                .delete(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // CREATE LANGUAGE SKILL
    $('#NewLanguageSkillButton').click(function (e) {
        e.preventDefault();
        createLanguageSkillModal.open();
    });

    createLanguageSkillModal.onResult(function () {
        location.reload();
    });

    // EDIT LANGUAGE SKILL
    $(document).on('click', '#UpdateLanguageSkillButton', function () {
        editLanguageSkillModal.open({ id: this.value });
    });

    editLanguageSkillModal.onResult(function () {
        location.reload();
    });

    // DELETE LANGUAGE SKILL
    $(document).on('click', '#DeleteLanguageSkillButton', function () {
        if (window.confirm(l('SkillDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.users.skills.languageSkill
                .delete(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // CREATE PROF SKILL
    $('#NewProfSkillButton').click(function (e) {
        e.preventDefault();
        createProfSkillModal.open();
    });

    createProfSkillModal.onResult(function () {
        location.reload();
    });

    // EDIT PROF SKILL
    $(document).on('click', '#UpdateProfSkillButton', function () {
        editProfSkillModal.open({ id: this.value });
    });

    editProfSkillModal.onResult(function () {
        location.reload();
    });

    // DELETE PROF SKILL
    $(document).on('click', '#DeleteProfSkillButton', function () {
        if (window.confirm(l('SkillDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.users.skills.profSkill
                .delete(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // CREATE JOB EXPERIENCE
    $('#NewJobButton').click(function (e) {
        e.preventDefault();
        createJobModal.open();
    });

    createJobModal.onResult(function () {
        location.reload();
    });

    // EDIT JOB EXPERIENCE
    $(document).on('click', '#UpdateJobButton', function () {
        editJobModal.open({ id: this.value });
    });

    editJobModal.onResult(function () {
        location.reload();
    });

    // DELETE JOB EXPERIENCE
    $(document).on('click', '#DeleteJobButton', function () {
        if (window.confirm(l('JobDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.users.jobExperience
                .delete(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // CREATE COURSE
    $('#NewCourseButton').click(function (e) {
        e.preventDefault();
        createCourseModal.open();
    });

    createCourseModal.onResult(function () {
        location.reload();
    });

    // EDIT COURSE
    $(document).on('click', '#UpdateCourseButton', function () {
        editCourseModal.open({ id: this.value });
    });

    editCourseModal.onResult(function () {
        location.reload();
    });

    // DELETE COURSE
    $(document).on('click', '#DeleteCourseButton', function () {
        if (window.confirm(l('CourseDeletionConfirmationMessage', this.name))) {
            daridakr.progGuru.users.userCourse
                .delete(this.value)
                .then(function () {
                    abp.notify.info(
                        l('SuccessfullyDeleted')
                    );
                    location.reload();
                });
        }
    });

    // SUBSCRIBE TO USER
    $(document).on('click', '#subscribe', function () {
        daridakr.progGuru.subscriptions.userSubscription
            .subscribe(this.name, this.value).then(function () {
                location.reload();
            });
    });

    // UNSUBSCRIBE FROM USER
    $(document).on('click', '#unsubscribe', function () {
        daridakr.progGuru.subscriptions.userSubscription
            .unsubscribe(this.name, this.value).then(function () {
                location.reload();
            });
    });
});