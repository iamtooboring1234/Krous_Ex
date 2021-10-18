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
        $('#gvDisc').DataTable({
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
})(jQuery);