$(document).ready(function () {
    var prev = "";
    var ajaxUserUrl = function () {
        return "DeleteUser/GetUsersAjax?userLogin=" + $('#searchName').val() + "&roleName=" + $('select[name=role]').val();
    }
    $('select[name="role"]').change(
            function () {
                if ($('#searchName').val() != "") {
                    GetEmployeeUsingAjax('users', ajaxUserUrl(), function () { $('#users').removeAttr('hidden'); });
                }
            });
    $('#searchName').keyup(
        function (e) {
            if ($('#searchName').val() != "") {
                if ($('#searchName').val() != prev) {
                    GetEmployeeUsingAjax('users', ajaxUserUrl(), function () { $('#users').removeAttr('hidden'); });
                }
            }
            else $('#users').attr('hidden','hidden');
            prev = $('#searchName').val();
        });
    $('form').submit(
        function () {            
            $('#searchName').val(findUser);
        }
        );
});
