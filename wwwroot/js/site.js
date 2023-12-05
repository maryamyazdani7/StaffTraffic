$(document).ready(function () {

    var table = $('#traffic-table').DataTable();
    $('.dataTables_length').addClass('bs-select');

    table.on('click', 'tbody tr', (e) => {
        GetTraffic($(e.currentTarget).attr('data-id'));
        $('#traffic-detail').attr('data-id', $(e.currentTarget).attr('data-id'));
        $('#traffic-detail').modal('show');
    });
    //#region PersianDatePicker
    $('.calender-btn').on('click', function () {
        $(this).parent().find('.persian-calendar').focus();
    });
    $(".persian-calendar").persianDatepicker({
        altField: '#normalAlt',
        altFormat: 'LLLL',
        initialValue: true,
        initialValueType: 'persian',
        observer: true,
        format: 'YYYY/MM/DD HH:mm',
        timePicker: { enabled: true },
        onSelect: function (event) {
            //$(this.model.inputElement).val(toEnNumber(this.model.PersianDate.date(event).format(this.model.options.format)).replace(/,/g, ''));
            //this.model.options.toolbox.todayButton.onToday(this.model);
        }


    })
    //#endregion

    $('#traffic-detail').on('hidden.bs.modal', function (event) {

        $('#traffic-detail').find('#traffic-detail-user').text('');
        $('#traffic-detail').find('#traffic-detail-indate').text('');
        $('#traffic-detail').find('#traffic-detail-outdate').text('');
        $('#traffic-detail').find('#traffic-detail-description').text('');
    })
});

const displayNotify = function (message, isError = false) {
    $('#notify').find('.toast-body').text(message)
    if (isError) {
        $('#notify').find('.toast-body').css('border-right-color', 'red');
    }
    var toastElList = [].slice.call(document.querySelectorAll('.toast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl)
    })
    toastList.forEach(toast => toast.show())
}

document.addEventListener("DOMContentLoaded", function (event) {

    const showNavbar = (toggleId, navId, bodyId, headerId) => {
        const toggle = document.getElementById(toggleId),
            nav = document.getElementById(navId),
            bodypd = document.getElementById(bodyId),
            headerpd = document.getElementById(headerId)

        // Validate that all variables exist
        if (toggle && nav && bodypd && headerpd) {
            toggle.addEventListener('click', () => {
                // show navbar
                nav.classList.toggle('navbar-show')
                // change icon
                toggle.classList.toggle('bx-x')
                // add padding to body
                bodypd.classList.toggle('body-pd')
                // add padding to header
                headerpd.classList.toggle('body-pd')
            })
        }
    }

    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header')

    /*===== LINK ACTIVE =====*/
    const linkColor = document.querySelectorAll('.nav_link')

    function colorLink() {
        if (linkColor) {
            linkColor.forEach(l => l.classList.remove('active'))
            this.classList.add('active')
        }
    }
    linkColor.forEach(l => l.addEventListener('click', colorLink))

    // Your code to run since DOM is loaded and ready
});


var DateTimeValidation = function (date, time) {
    let datePattern = /^[1-4]\d{3}\/((0[1-6]\/((3[0-1])|([1-2][0-9])|(0[1-9])))|((1[0-2]|(0[7-9]))\/(30|([1-2][0-9])|(0[1-9]))))$/;
    let dateValidation = datePattern.test(date);

    let timePattern = /^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$/i;
    let timeValidation = timePattern.test(time);
    return [dateValidation, timeValidation]

}

const toEnNumber = function (inputNumber) {
    if (inputNumber == undefined) return '';
    var str = inputNumber.toString().trim();
    if (str == "") return "";
    str = str.replace(/۰/img, '0');
    str = str.replace(/۱/img, '1');
    str = str.replace(/۲/img, '2');
    str = str.replace(/۳/img, '3');
    str = str.replace(/۴/img, '4');
    str = str.replace(/۵/img, '5');
    str = str.replace(/۶/img, '6');
    str = str.replace(/۷/img, '7');
    str = str.replace(/۸/img, '8');
    str = str.replace(/۹/img, '9');
    return str;
}
var checkTrafficInsertEditRequired = function () {
    var inputIds = "";

    if ($('#user-select').val() == "" || $('#enter-date').val() == "" || $('#out-date').val() == "") {
        displayNotify("لطفا همه فیلدها را پر کنید.", true);
        return false;
    }

    if ($('#enter-date').val() != "") {
        var enterDate = toEnNumber($('#enter-date').val().replace(/,/g, ''));
        var validationEnterDate = DateTimeValidation(enterDate.split(' ')[0], enterDate.split(' ')[1])

        if (!validationEnterDate[0] && !validationEnterDate[1]) {
            displayNotify("فرمت زمان ورود نادرست است!", true);
            return false;
        }
        else if (!validationEnterDate[0] ||
            enterDate.split(' ')[0].replace(/[^\d]/g, "") < 13000000) {
            displayNotify("فرمت تاریخ ورود نادرست است!", true);
            return false;
        }
        else if (!validationEnterDate[1]) {
            displayNotify("فرمت ساعت ورود نادرست است!", true);
            return false;
        }

    }

    if ($('#out-date').val() != "") {

        var outDate = toEnNumber($('#out-date').val().replace(/,/g, ''));
        var validationOutDate = DateTimeValidation(outDate.split(' ')[0], outDate.split(' ')[1])

        if (!validationOutDate[0] && !validationOutDate[1]) {
            displayNotify("فرمت زمان خروج نادرست است!", true);
            return false;
        }
        else if (!validationOutDate[0] ||
            outDate.split(' ')[0].replace(/[^\d]/g, "") < 13000000) {
            displayNotify("فرمت تاریخ خروج نادرست است!", true);
            return false;
        }
        else if (!validationOutDate[1]) {
            displayNotify("فرمت ساعت خروج نادرست است!", true);
            return false;
        }

    }

    if ($('#enter-date').val() != "" && $('#out-date').val() != "" &&
        $.isNumeric(enterDate.replace(/[^\d]/g, "")) &&
        $.isNumeric(outDate.replace(/[^\d]/g, "")) &&
        enterDate.replace(/[^\d]/g, "") >= outDate.replace(/[^\d]/g, "")) {
        displayNotify("زمان خروج نمیتواند زودتر از زمان ورود باشد!", true);
        return false;
    }
    return true;
}

var InsertTraffic = function () {
    if (!checkTrafficInsertEditRequired()) return
    var outDate = toEnNumber($('#out-date').val().replace(/,/g, ''));
    var enterDate = toEnNumber($('#enter-date').val().replace(/,/g, ''));
    var object = {
        UserId: $('#user-select').val(),
        OutDate: outDate,
        InDate: enterDate,
        Description: $('#description').val()
    }
    $('#insert-traffic').find('.spinner-border').removeClass('d-none');
    $.ajax({
        type: 'POST',
        url: '/api/traffic',
        data: JSON.stringify(object),
        contentType: "application/json; charset=utf-8",
    })
        .done(function (data) {
            $('#insert-traffic').find('.spinner-border').addClass('d-none');
            location.reload()
        })
        .fail(function (data) {
            $('#insert-traffic').find('.spinner-border').addClass('d-none');
            displayNotify("خطا در انجام عملیات", true);
        });

}



var GetTraffic = function (id) {
    $.ajax({
        type: 'Get',
        url: '/api/traffic/' + id,
        contentType: "application/json; charset=utf-8",
    })
        .done(function (data) {
            $('#traffic-detail').find('#traffic-detail-user').text(data.user.firstName + " " + data.user.lastName);
            $('#traffic-detail').find('#traffic-detail-indate').text(data.inDatePersian);
            $('#traffic-detail').find('#traffic-detail-outdate').text(data.outDatePersian);
            $('#traffic-detail').find('#traffic-detail-description').text(data.description);
        })
        .fail(function (data) {
            displayNotify("خطا در انجام عملیات", true);
        });

}

var DeleteTraffic = function (id) {
    $('#traffic-detail').find('.spinner-border').removeClass('d-none');

    $.ajax({
        type: 'Delete',
        url: '/api/traffic/' + id,
        contentType: "application/json; charset=utf-8",
    })
        .done(function (data) {
            $('#traffic-detail').find('.spinner-border').addClass('d-none');

            if (data) {
                location.reload()
            } else {
                displayNotify("خطا در انجام عملیات", true);
            }
        })
        .fail(function (data) {
            $('#traffic-detail').find('.spinner-border').addClass('d-none');

            displayNotify("خطا در انجام عملیات", true);
        });

}