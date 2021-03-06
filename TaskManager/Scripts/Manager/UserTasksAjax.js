$(document).ready(function () {
    $('#taskSelect').submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        var url = $(this).attr('action');
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            success: function (data) {
                var task = '<input data-val="true" data-val-required="The Guid field is required." hidden="hidden" id="Guid" name="Guid" type="text" value="';
                task += data.Id;
                task += '" /><p><label for="Title">Title</label><br><input data-val="true" data-val-required="The Title field is required." id="Title" name="Title" type="text" value="';
                task += data.Title;
                task += '" /></p><p><label for="Description">Description</label><br><textarea cols="20" data-val="true" data-val-required="The Description field is required." id="Description" name="Description" rows="2">';
                task += data.Description;
                task += '</textarea></p>';

                $('#TaskEditor').append(task);

                $('#taskSelector').attr('hidden', 'hidden');
                $('#editor').removeAttr('hidden');
            }
        });
    });
});