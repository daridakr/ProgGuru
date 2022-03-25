$(document).ready(function () {
    var l = abp.localization.getResource('ProgGuru');

    // CREATE PROJECTS DATATABLE
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

    // CREATE ARTICLES DATATABLE
    //var dataArticlesTable = $('table#ArticlesTable').DataTable({
    //    "language": {
    //        lengthMenu: l('lengthMenu'),
    //        zeroRecords: l('zeroRecords'),
    //        info: l('info'),
    //        search: l('DataSearch'),
    //        paginate: {
    //            first: l('first'),
    //            last: l('last'),
    //            next: l('next'),
    //            previous: l('previous')
    //        }
    //    },
    //});

    // PRINT PROJECTS TABLE
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

    // PRINT ARTICLES TABLE
    const tableArticlesToPrint = document.getElementById('ArticlesTable');

    function printArticlesData(tableArticlesToPrint) {
        Popup($(tableArticlesToPrint).html());
    }

    function Popup(data) {
        const mywindow = window.open('', 'ArticlesTable', 'height=600, width=1000');
        mywindow.document.write('<link rel="stylesheet" href="styles.css" type="text/css" />');
        mywindow.document.write(tableArticlesToPrint.outerHTML);
        mywindow.document.close(); // для IE >= 10 
        mywindow.focus();          // для IE >= 10 
        mywindow.print();
        mywindow.close();
        return true;
    }

    $(document).on('click', '#printArticlesTable', function () {
        printArticlesData();
        return false;
    });

    // COPY PROJECTS TABLE
    (function () {
        new Clipboard('#copy-button');
    })();

    // SUBSCRIBE TO GROUP
    $(document).on('click', '#subscribe', function () {
        daridakr.progGuru.subscriptions.groupSubscription
            .subscribe(this.name, this.value).then(function () {
                location.reload();
            });
    });

    // UNSUBSCRIBE FROM GROUP
    $(document).on('click', '#unsubscribe', function () {
        daridakr.progGuru.subscriptions.groupSubscription
            .unsubscribe(this.name, this.value).then(function () {
                location.reload();
            });
    });
});