app.register("app.mineReportsList",
    {
        init: function (initData) {
            this.initData = initData;
        },
        onSelect: function (e) {
            $(e.contentElement).html("");
        },
        onResizeEnd: function(e) {
            $('#new-request-window').data('kendoWindow').center();
        },
        onCreate: function () {
            var window = $('#new-request-window').data('kendoWindow');
            window.center().open();
        },
        onDownload: function (e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            $.post(app.mineReportsList.initData.CanDownloadReportUrl,
                { requestId: dataItem.ReportRequestId },
                function(result) {
                    if (result.Success) {
                        window.location.href = app.mineReportsList.initData.DownloadReportUrl +
                            '?requestId=' +
                            dataItem.ReportRequestId;
                    } else {
                        app.notify.error(result.Message);
                    }
                });
        },
        buttons: {
            accept: function() {
                var grid = $('#all-patients-grid').data('kendoGrid');
                var dataItem = grid.dataItem(grid.select());
                $.post(app.mineReportsList.initData.CreateNewRequestUrl,
                    { patientPesel: dataItem.Pesel, patientFirstName: dataItem.FirstName, patientLastName: dataItem.LastName },
                    function(result) {
                        if (result.Success) {
                            $('#new-request-window').data('kendoWindow').close();
                            var pendingGrid = $('#pending-report-requests').data('kendoGrid');
                            app.notify.success("Zlecenie zostało dodane");
                            pendingGrid.dataSource.read();
                            pendingGrid.refresh();
                        } else {
                            app.notify.error(result.Message);
                            $('#new-request-window').data('kendoWindow').close();
                        }
                    });
            },
            cancel: function() {
                $('#new-request-window').data('kendoWindow').close();
            }
        }
    });