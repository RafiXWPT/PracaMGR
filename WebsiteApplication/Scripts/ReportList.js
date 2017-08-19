app.register("app.reportList",
    {
        onSelect: function(e) {
            $(e.contentElement).html("");
        },
        onAccept: function(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            $.post('@Url.Action("AcceptRequest", "ReportList")',
                { requestId: dataItem.ReaportRequestId },
                function(result) {
                    if (result.Success) {
                        app.notify.success("Prośba zaakceptowana.");
                    } else {
                        app.notify.error(result.Message);
                    }
                });
        },
        onReject: function(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            $.post('@Url.Action("RejectRequest", "ReportList")',
                { requestId: dataItem.ReaportRequestId },
                function(result) {
                    if (result.Success) {
                        app.notify.success("Prośba odrzucona.");
                    } else {
                        app.notify.error(result.Message);
                    }
                });
        }
    });