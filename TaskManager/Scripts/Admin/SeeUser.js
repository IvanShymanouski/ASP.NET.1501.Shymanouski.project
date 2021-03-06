var ajaxUserUrl = function () {
    return "GetUsersAjax?userLogin=" + $('#searchName').val();
}
var ajaxRoleUrl = function () {
    return "SeeUserRolesAjax?userLogin=" + $('#searchName').val();
}

$(document).ready(function () {
    $('#searchName').keyup(show_users_help);
    $('form').submit(
        function (e) {
            e.preventDefault();
            if ($('#searchName').val() != findUser) show_users_help();
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