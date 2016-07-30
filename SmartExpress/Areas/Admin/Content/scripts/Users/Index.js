$(function () {
    $('#users-grid').DataTable({});

    $(".user-delete").click(function () {
        if (confirm("Do you really want to delete?")) {
            return true;
        }
        return false;
    });
});