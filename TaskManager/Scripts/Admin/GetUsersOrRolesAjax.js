var findUser = undefined;
var prev = "";

function show_users_help(e) {
    if ($('#searchName').val() != prev) {
        $('#roleinfo b').empty();
        $('#roles').attr('hidden', 'hidden');
        if ($('#searchName').val() != "") {
            GetEmployeeUsingAjax('users', ajaxUserUrl(), function () { $('#users').removeAttr('hidden'); });
        }
        else {
            $('#users').attr('hidden', 'hidden');
            findUser = undefined;
        }
        prev = $('#searchName').val();
    }
}

function GetEmployeeUsingAjax(item, url, callback) {
    $.getJSON(url, null,
        function (items) {
            $('#' + item + ' ul').empty();
            $('#' + item + ' span').empty();
            var data = '';
            var length = 0
            for (var i in items) {
                data += "<li>" + items[i] + "</li>";
                length++;
            }
            if (length < 2) {
                findUser = (length == 1)?items[0]:undefined;
            }
            else findUser = null;
            $('#' + item + ' span').append(length);
            $('#' + item + ' ul').append(data);
            if (typeof callback != 'undefined') callback(length);
        }
    );
}