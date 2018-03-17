app.register("app.reportsList",
    {
        init: function(initData) {
            this.initData = initData;
        },
        onSelect: function(e) {
            $(e.contentElement).html("");
        },
        onAccept: function(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            var grid = this;
            $.post(app.reportsList.initData.AcceptRequestUrl,
                { requestId: dataItem.ReportRequestId },
                function(result) {
                    if (result.Success) {
                        app.notify.success("Prośba zaakceptowana.");
                        grid.dataSource.read();
                        grid.refresh();
                    } else {
                        app.notify.error(result.Message);
                    }
                });
        },
        onReject: function(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            var grid = this;
            $.post(app.reportsList.initData.RejectRequestUrl,
                { requestId: dataItem.ReportRequestId },
                function(result) {
                    if (result.Success) {
                        app.notify.success("Prośba odrzucona.");
                        grid.dataSource.read();
                        grid.refresh();
                    } else {
                        app.notify.error(result.Message);
                    }
                });
        }
    });