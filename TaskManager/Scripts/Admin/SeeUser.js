$(document).ready(function () {
    var prev = "";    
    var ajaxUserUrl = function () {
        return "GetUsersAjax?userLogin=" + $('#searchName').val();
    }
    var ajaxRoleUrl = function () {
        return "SeeUserRolesAjax?userLogin=" + $('#searchName').val();
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
    $('form').submit(
        function (e) {
            e.preventDefault();
            $('#roleinfo b').empty();
            if (null == findUser) {
                if (null === findUser) $('#roleinfo b').append('More than one user match');
                else $('#roleinfo b').append('No one user match');                
            } else {
                if ($('#roles').attr('hidden') == 'hidden') {
                    $('#roleinfo b').val('');
                    $('#searchName').val(findUser);
                    prev = $('#searchName').val();
                    GetEmployeeUsingAjax('roles', ajaxRoleUrl(), rolesForUser);
                }
            }
        });
    function rolesForUser(){        
        $('#roles span').append(' roles for ' + $('#searchName').val() + ':')
        $('#roles').removeAttr('hidden');
    }
});