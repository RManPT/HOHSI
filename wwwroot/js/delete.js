$((function () {
    var url;
    var redirectUrl;
    var target;

    $('body').append(`
                    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" area-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                        <div class="modal-header ">
                            <h5 class="modal-title" id="myModalLabel">Warning</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                         <p>&nbsp;</p>
                        <div class="modal-body delete-modal-body">
                        </div>
                        <p>&nbsp;</p>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal" id="cancel-action">Cancel</button>
                            <button type="button" class="btn btn-danger" id="confirm-action">Confirm</button>
                        </div>
                        </div>
                    </div>
                    </div>`);

    //Delete Action
    $(".confirmation").on('click', (e) => {
        e.preventDefault();

        target = e.target;
        var Id = $(target).data('id');
        var controller = $(target).data('controller');
        var action = $(target).data('action');
        var bodyMessage = $(target).data('body-message');
        redirectUrl = $(target).data('redirect-url');

        if (Id != null) url = "/" + controller + "/" + action + "?Id=" + Id
        else url = "/" + controller + "/" + action;
        console.log(url);
        $(".delete-modal-body").text(bodyMessage);
        $("#deleteModal").modal('show');
    });



    $("#confirm-action").on('click', () => {
        $.get(url)
            .done((result) => {
                if (!redirectUrl) {
                    return $(target).parent().parent().remove();
                }
                window.location.href = redirectUrl;
            })
            .fail((error) => {
                if (redirectUrl)
                    window.location.href = redirectUrl;
            }).always(() => {
                $("#deleteModal").modal('hide');
            });
    });

}()));