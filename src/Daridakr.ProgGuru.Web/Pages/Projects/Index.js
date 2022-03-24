$(document).ready(function () {
    var l = abp.localization.getResource('ProgGuru');
    //var createModal = new abp.ModalManager(abp.appPath + 'Projects/Admin/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Projects/Admin/EditModal');

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

    // EDIT DATA
    $(document).on('click', '#UpdateProjectButton', function () {
        editModal.open({ id: this.value });
    });

    editModal.onResult(function () {
        location.reload();
    });

    //// CREATE NEW
    //$('#NewProjectButton').click(function (e) {
    //    e.preventDefault();
    //    createModal.open();
    //});

    //createModal.onResult(function () {
    //    location.reload();
    //});

    // DELETE
    $(document).on('click', '#DeleteProjectButton', function () {
        if (window.confirm(l('AuthorDeletionConfirmationMessage', this.name))) {
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
});