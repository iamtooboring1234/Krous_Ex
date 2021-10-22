(function($) {
    'use strict';
    $(function() {
    $('#order-listings').DataTable({
        "aLengthMenu": [
        [5, 10, 15, -1],
        [5, 10, 15, "All"]
        ],
        "iDisplayLength": 10,
        "language": {
        search: ""
        } 
    });
    });
    $(function () {
        $('#order-listing').DataTable({
            "aLengthMenu": [
                [5, 10, 15, -1],
                [5, 10, 15, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            }
        });
    });
    $(function () {
        $('#gvFAQ').DataTable({
            "searching": false,
            "aLengthMenu": [
                [1, 5, 10, 15, -1],
                [1, 5, 10, 15, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            }
        });
    });
    $(function () {
        $('#gvForumReport').DataTable({
            "searching": false,
            "aLengthMenu": [
                [1, 5, 10, 15, -1],
                [1, 5, 10, 15, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            }
        });
    });
    $(function () {
        $('#gvDisc').DataTable({
            "ordering": false,
            "aLengthMenu": [
                [1, 5, 10, 15, -1],
                [1, 5, 10, 15, "All"]
            ],
            columnDefs: [{
                'targets': [0], /* column index [0,1,2,3]*/
                'orderable': false, /* true or false */
            }],
            "order": [[1, 'asc']],
            "iDisplayLength": 10,
            "language": {
                search: ""
            }
        });
    });
    $(function () {
        $('#gvReply').DataTable({
            "searching": false,
            "ordering": false,
            "bInfo": false,
            "aLengthMenu": [
                [2, 10, 20, 40, 50, -1],
                [2, 10, 20, 40, 50, "All"]
            ],
            columnDefs: [{
                'targets': [0], /* column index [0,1,2,3]*/
                'orderable': false, /* true or false */
            }],
            "iDisplayLength": 20,
            "language": {
                "search": "",
                "emptyTable": "Currently, this discussion has no reply."
            }
        });
    });
})(jQuery);