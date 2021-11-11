(function ($) {
    showAddSuccessToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Item Added Successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showUpdateSuccessToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Item Updated Successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showDeleteSuccessToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Item Deleted Successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showNotificationSentSuccess = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Notification is successfully sent to the users.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showSuccessToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Item Added Successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showFileUploadedSuccessfulAndAttendanceTaked = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Your answer is uploaded sucessfully. Your attendance is automatically taken by the system.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showInfoToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Info',
            text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
            showHideTransition: 'slide',
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
    };
    showWarningToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Warning',
            text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
            showHideTransition: 'slide',
            icon: 'warning',
            loaderBg: '#57c7d4',
            position: 'top-right'
        })
    };
    showDangerToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Danger',
            text: 'File is required.',
            showHideTransition: 'slide',
            icon: 'error',
            loaderBg: '#f2a654',
            position: 'top-right'
        })
    };
    showToastPosition = function (position) {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Positioning',
            text: 'Specify the custom position object or use one of the predefined ones',
            position: String(position),
            icon: 'info',
            stack: false,
            loaderBg: '#f96868'
        })
    };
    showToastInCustomPosition = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Custom positioning',
            text: 'Specify the custom position object or use one of the predefined ones',
            icon: 'info',
            position: {
                left: 120,
                top: 120
            },
            stack: false,
            loaderBg: '#f96868'
        })
    };
    resetToastPosition = function () {
        $('.jq-toast-wrap').removeClass('bottom-left bottom-right top-left top-right mid-center'); // to remove previous position class
        $(".jq-toast-wrap").css({
            "top": "",
            "left": "",
            "bottom": "",
            "right": ""
        }); //to remove previous position style
    };
    showReportCommentsSuccesfully = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Selected comment is reported successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showDeleteCommentsSuccesfully = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Comment is deleted successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
    showUpdateCommentsSuccesfully = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: 'Comment is updated successfully.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: 'top-right'
        })
    };
})(jQuery);