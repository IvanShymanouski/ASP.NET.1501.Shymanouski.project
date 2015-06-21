$(document).ready(function () {
    var prev = "";    
    var ajaxUserUrl = function () {
        return "GetUsersAjax?userLogin=" + $('#searchName').val();
    }
    var ajaxRoleUrl = function () {
        return actionName+"?userLogin=" + $('#searchName').val();
    }
    $('#searchName').keyup(
        function (e) {
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
        });
    $('#selectUser').submit(
        function (e) {
            e.preventDefault();
            $('#roleinfo b').empty();
            if (null == findUser) {
                if (null === findUser) $('#roleinfo b').append('More than one user match');
                else $('#roleinfo b').append('No one user match');                
            } else {
                if ($('#roles').attr('hidden') == 'hidden') {
                    $('#searchName').val(findUser);
                    prev = $('#searchName').val();
                    GetRoleAjax(ajaxRoleUrl(), rolesForUser);
                }
            }
        });
    function rolesForUser(length) {
        if (length > 0) {
            $('#roles span').append(' roles for ' + $('#searchName').val() + ':')
            $('#roles').removeAttr('hidden');
        }
        else {
            $('#roleinfo b').append(accountMessage);
        }
    }
});

var findUser = undefined;

function GetRoleAjax(url, callback) {
    $.getJSON(url, null,
        function (items) {
            $('#rolesToUser').empty();
            var data = '';
            var length = 0            
            for (var i in items) {
                data += '<input name="roles" type="checkbox" value="'+items[i]+'" />'
                         + items[i] + '<br>';
                length++;
            }
            $('#userLogin').val($('#searchName').val());
            $('#rolesToUser').append(data);
            if (typeof callback != 'undefined') callback(length);
        }
    );
}