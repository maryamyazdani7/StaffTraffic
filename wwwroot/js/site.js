$(document).ready(function () {

    var table = $('#traffic-table').DataTable();
    $('.dataTables_length').addClass('bs-select');

    table.on('click', 'tbody tr', (e) => {
        //displayNotify("hhhhh")
        //$('#traffic-detail').attr('data-id', $(e.currentTarget).attr('data-id'))
        //$('#traffic-detail').modal('show');
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
        timePicker: { enabled: false },
        onSelect: function (event) {
            //$(this.model.inputElement).val(toEnNumber(this.model.PersianDate.date(event).format(this.model.options.format)).replace(/,/g, ''));
            //this.model.options.toolbox.todayButton.onToday(this.model);
        }
    });
    //$(".persian-calendar").val("");
    //$("#traffic-page").find('input.date-mask').inputmask("datetime", {
    //    mask: "9999/99/99",
    //    placeholder: "----/--/--"
    //});
    //$("#traffic-insert-edit").find('input.date-mask').inputmask("datetime", {
    //    mask: "9999/99/99 99:99",
    //    placeholder: "----/--/-- --:--"
    //});
    //#endregion
});

const displayNotify = function (message) {
    $('#notify').find('.toast-body').text(message)

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
        displayNotify("لطفا همه فیلدها را پر کنید.");
        return false;
    }

    if ($('#enter-date').val() != "") {
        var enterDate = toEnNumber($('#enter-date').val().replace(/,/g, ''));
        var validationEnterDate = DateTimeValidation(enterDate[0], enterDate.split(' ')[1])

        if (!validationEnterDate[0] && !validationEnterDate[1]) {
            displayNotify("فرمت زمان ورود نادرست است!");
            return false;
        }
        else if (!validationEnterDate[0] ||
            enterDate.split(' ')[0].replace(/[^\d]/g, "") < 13000000) {
            displayNotify("فرمت تاریخ ورود نادرست است!");
            return false;
        }
        else if (!validationEnterDate[1]) {
            displayNotify("فرمت ساعت ورود نادرست است!");
            return false;
        }

    }

    if ($('#out-date').val() != "") {

        var outDate = toEnNumber($('#out-date').val().replace(/,/g, ''));
        var validationOutDate = DateTimeValidation(outDate.split(' ')[0], outDate.split(' ')[1])

        if (!validationOutDate[0] && !validationOutDate[1]) {
            displayNotify("فرمت زمان خروج نادرست است!");
            return false;
        }
        else if (!validationOutDate[0] ||
            outDate.split(' ')[0].replace(/[^\d]/g, "") < 13000000) {
            displayNotify("فرمت تاریخ خروج نادرست است!");
            return false;
        }
        else if (!validationOutDate[1]) {
            displayNotify("فرمت ساعت خروج نادرست است!");
            return false;
        }

    }

    if ($('#enter-date').val() != "" && $('#out-date').val() != "" &&
        $.isNumeric($('#enter-date').val().replace(/[^\d]/g, "")) &&
        $.isNumeric($('#out-date').val().replace(/[^\d]/g, "")) &&
        $('#enter-date').val().replace(/[^\d]/g, "") >= $('#out-date').val().replace(/[^\d]/g, "")) {
        displayNotify("زمان خروج نمیتواند زودتر از زمان ورود باشد!");
        return false;
    }
    return true;
}

var InsertTraffic = function () {
    if (!checkTrafficInsertEditRequired()) return;
    var object = {
        UserId: $('#user-select').val(),
        OutDate: $('#out-date').val(),
        InDate: $('#enter-date').val(),
        Description: $('#description').val()
    }
    $.ajax({
        type: 'POST',
        url: '/api/traffic',
        data: object
    })
        .done(function (data) {
            $('.loader-container').addClass('d-none');
            doneCallback(data);
        })
        .fail(function (data) {
            $('.loader-container').addClass('d-none');
            failCallback();
        });

}