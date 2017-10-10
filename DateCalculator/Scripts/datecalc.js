$(document).ready(function () {
    var datenumber = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15",
                 "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"];
    var monthslist = ["01 - Jan", "02 - Feb", "03 - Mar", "04 - Apr", "05 - May", "06 - Jun", "07 - Jul", "08 - Aug",
        "09 - Sep", "10 - Oct", "11 - Nov", "12 - Dec"];
    var yearlist = ["2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021"];

    //$.ui.autocomplete.prototype._renderMenu = function (ul, items) {
    //    var self = this;
    //    ul.append("<table><tbody></tbody></table>");
    //    $.each(items, function (index, item) {
    //        if (index <= 15) self._renderItem(ul.find("table tbody"), item);
    //        else self._renderItem(ul.find("table tbody tr"), item);
    //    });
    //};

    //$.ui.autocomplete.prototype._renderItem = function (table, item) {
    //    if (parseInt(item.value) <= 16) {
    //        return $("<tr></tr>")
    //            .data("item.autocomplete", item)
    //            .append("<td id='firsttd' style='padding:2px 30px 5px 2px'>" + item.value + "</td>" + "<td id=" + "secondtd" + parseInt(item.value) + "></td>")
    //            .appendTo(table);
    //    }
    //    else {
    //        return $("#secondtd" + ((parseInt(item.value) - 16)))
    //            .data("item.autocomplete", item)
    //            .append(item.value);
    //    }
    //};

    $('#firstdate,#seconddate,#birthdate,#asofnowdate').autocomplete(
    {
        source: function (request, response) {
            response(datenumber);
        },
        minLength: 0,
        scroll: true
    }).bind('focus', function () { $(this).autocomplete("search"); });


    $('#firstmonth').autocomplete(
   {
       source: function (request, response) {
           response(monthslist);
       },
       minLength: 0,
       select: function (event, ui) {
           event.preventDefault();
           $('#firstmonth').val(ui.item.value.substring(0, 2));
       }
   }).bind('focus', function () { $(this).autocomplete("search"); });

    $('#secondmonth').autocomplete(
    {
        source: function (request, response) {
            response(monthslist);
        },
        minLength: 0,
        select: function (event, ui) {
            event.preventDefault();
            $('#secondmonth').val(ui.item.value.substring(0, 2));
        }
    }).bind('focus', function () { $(this).autocomplete("search"); });

    $('#birthmonth').autocomplete(
    {
        source: function (request, response) {
            response(monthslist);
        },
        minLength: 0,
        select: function (event, ui) {
            event.preventDefault();
            $('#birthmonth').val(ui.item.value.substring(0, 2));
        }
    }).bind('focus', function () { $(this).autocomplete("search"); });

    $('#asofnowmonth').autocomplete(
    {
        source: function (request, response) {
            response(monthslist);
        },
        minLength: 0,
        select: function (event, ui) {
            event.preventDefault();
            $('#asofnowmonth').val(ui.item.value.substring(0, 2));
        }
    }).bind('focus', function () { $(this).autocomplete("search"); });

    $('#firstyear, #secondyear,#birthyear,#asofnowyear').autocomplete(
    {
        source: function (request, response) {
            response(yearlist);
        },
        minLength: 0
    }).bind('focus', function () { $(this).autocomplete("search"); });

    $("#firstdatepicker").datepicker({
        showOn: "both",
        buttonImage: '/images/calender.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        showAnim: "fade",
        yearRange: '1980:2030',
        onSelect: function (selected) {
            $('#firstdate').val(selected.split('-')[0]);
            $('#firstmonth').val(selected.split('-')[1]);
            $('#firstyear').val(selected.split('-')[2]);
        }
    });

    $("#seconddatepicker").datepicker({
        showOn: "both",
        buttonImage: '/images/calender.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        showAnim: "fade",
        yearRange: '1980:2030',
        onSelect: function (selected) {
            $('#seconddate').val(selected.split('-')[0]);
            $('#secondmonth').val(selected.split('-')[1]);
            $('#secondyear').val(selected.split('-')[2]);
        }
    });

    $("#birthdatepicker").datepicker({
        showOn: "both",
        buttonImage: '/images/calender.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        showAnim: "fade",
        yearRange: '1950:2050',
        onSelect: function (selected) {
            $('#birthdate').val(selected.split('-')[0]);
            $('#birthmonth').val(selected.split('-')[1]);
            $('#birthyear').val(selected.split('-')[2]);
        }
    });

    $("#asofnowdatepicker").datepicker({
        showOn: "both",
        buttonImage: '/images/calender.png',
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        showAnim: "fade",
        yearRange: '1950:2050',
        onSelect: function (selected) {
            $('#asofnowdate').val(selected.split('-')[0]);
            $('#asofnowmonth').val(selected.split('-')[1]);
            $('#asofnowyear').val(selected.split('-')[2]);
        }
    });

    //TODO Not Working
    $('#ResultRefineOption').change(function () {
        RefineOption();
    });
});

function RefineOption() {
    var resultRefine = $("#ResultRefineOption option:selected").text();
    if (resultRefine === "Exclude") {
        $("#Option option[value='alldays']").attr("disabled", true);
        $("#Option option[value='nodays']").attr("disabled", false);
        $('#Option option[value="nodays"]').prop('selected', true);
    }
    else if (resultRefine.replace(/ /g, '') === "IncludeOnly") {
        $("#Option option[value='alldays']").attr("disabled", false);
        $("#Option option[value='nodays']").attr("disabled", true);
        $('#Option option[value="weekends"]').prop('selected', true);
    }
}

function filltextbox(start) {
    var currentTime = new Date();
    var month = (currentTime.getMonth() + 1) < 10 ? "0" + (currentTime.getMonth() + 1) : (currentTime.getMonth() + 1);
    var date = currentTime.getDate() < 10 ? "0" + currentTime.getDate() : currentTime.getDate();
    var year = currentTime.getFullYear();
    if (start === "start") {
        $('#firstdate').val(date);
        $('#firstmonth').val(month);
        $('#firstyear').val(year);
    }
    else if (start === "end") {
        $('#seconddate').val(date);
        $('#secondmonth').val(month);
        $('#secondyear').val(year);
    }
    else if (start == "asofnow") {
        $('#asofnowdate').val(date);
        $('#asofnowmonth').val(month);
        $('#asofnowyear').val(year);
    }
}
