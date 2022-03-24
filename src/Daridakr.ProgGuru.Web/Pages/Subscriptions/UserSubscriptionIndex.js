$(document).ready(function () {
    var l = abp.localization.getResource('ProgGuru');

    // CREATE DATATABLE
    var dataTable = $('table#UserSubscriptionsTable').DataTable({
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
    const tableToPrint = document.getElementById('UserSubscriptionsTable');

    function printData(tableToPrint) {
        Popup($(tableToPrint).html());
    }

    function Popup(data) {
        const mywindow = window.open('', 'UserSubscriptionsTable', 'height=600, width=1000');
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

    // DELETE
    $(document).on('click', '#unsubscribe', function () {
        if (window.confirm(l('AdminUnsubscribeConfirmationMessage'))) {
            daridakr.progGuru.subscriptions.userSubscription
                .unsubscribe(this.name, this.value).then(function () {
                    location.reload();
                });
        }
    });
});