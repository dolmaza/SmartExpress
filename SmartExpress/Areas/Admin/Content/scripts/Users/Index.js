$(function () {
    $('#users-grid').DataTable({});

    $(".user-delete").click(function () {
        if (confirm(ConfirmDeleteText)) {
            return true;
        }
        return false;
    });
});