var action = '';

$(document).ready(function () {
    action = $('#registrForm').attr('action');
    $('#registrForm').attr('action', action + '?role=' + $('#roles').attr('value'));
    $('input:radio[name="roles"]').change(
    function () {
        $('#registrForm').attr('action', action + '?role=' + $(this).attr('value'));
    });
});