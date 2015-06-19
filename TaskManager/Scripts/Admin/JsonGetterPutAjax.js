var helperUserObj = { user: '', role: '' };
var isDelete = false;

$(document).ready(function () {    
    
    $('input:radio[name="deleteOrChange"]').change(
    function () {
        if ($(this).is(':checked')) {
            if ($(this).attr('value') == 'delete') {
                isDelete = true;
                $('#replaceLink').empty();
                $('#replaceLink').append('Delete user');
                $('#ChsRoleForRepl').attr('hidden','hidden');
             }
            else {
                isDelete = false;
                $('#replaceLink').empty();
                $('#replaceLink').append('Change role');
                $('#ChsRoleForRepl').removeAttr('hidden');
                $('#replaceLink').attr('hidden', 'hidden');
            }
            replaceModifyLink();
        }
    });
    $('input:radio[name="roles"]').change(
    function () {
        if ($(this).is(':checked')) {
            $('#SelectedRole').empty();
            $('#SelectedRole').append('Users in "' + $(this).attr('value') + '" role:');
            GetEmployeeUsingAjax($(this).attr('value'));
        }
    });
    $('input:radio[name="replace"]').change(
    function () {
        if ($(this).is(':checked')) {
            helperUserObj.role = $(this).attr('value');
            replaceModifyLink();
        }
    });
});

function replaceModifyLink() {
    if (!isDelete) {
        $('#replaceLink').attr('href', 'UserRoles/ReplaseRole?user=' + helperUserObj.user +
                                       '&role=' + helperUserObj.role);        
    }
    else {
        $('#replaceLink').attr('href', 'UserRoles/DeleteUser?user=' + helperUserObj.user);
    }
    if (helperUserObj.user !== '' && (helperUserObj.role !== '' || isDelete)) $('#replaceLink').removeAttr('hidden');
}

function GetEmployeeUsingAjax(roleName) {
    $.getJSON("UserRoles/GetUsersAjax?roleName="+roleName,null,
        function (emp) {
            helperUserObj.user = '';
            $('#JsonGetter').empty();
            for (var i = 0; i < emp.length; i++) {
                $('#JsonGetter').append('<input name="userToModify" \
                                                type="radio" value="' + emp[i].Login + '" /> \
                                         <span>Login: ' + emp[i].Login + '</span> <br />'
                );
            }
            $('input:radio[name="userToModify"]').change(
            function () {
                if ($(this).is(':checked')) {
                    helperUserObj.user = $(this).attr('value');
                    replaceModifyLink();
                }
            });
            $('#selectReplace').removeAttr('hidden');
            $('#replaceLink').attr('hidden','hidden');
        }
    );
}