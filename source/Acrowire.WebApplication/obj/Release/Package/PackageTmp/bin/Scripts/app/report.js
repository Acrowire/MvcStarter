$(function () {

    //alert('Report name: ' + window.ReportName + '?' + window.Clientparams);
    var url = window.ReportName+'?' + window.Clientparams;
    //var url = 'Report_WeeklyClientHoursData?ClientName=' + window.clientName + '&weekname=' + window.weekName;
    //var url = 'Report_ClientHoursData?ClientName=' + window.clientName;

    $.get(url, function (data, ts, error) {
        var r = {};
        var ax = [];
        var month_hours = {};
        var total = 0;


        // Graph Data 
        for (var index in data) {
            var row = data[index];

            // Hierarchy 
            if (!r[row.company_name]) {
                r[row.company_name] = {};
                r[row.company_name].total_hours = 0;
            }

            if (!r[row.company_name][row.project_name]) {
                r[row.company_name][row.project_name] = {};
            }

            var year = moment(row.date).format("YYYY");
            var month = moment(row.date).format("MMMM");

            if (!r[row.company_name][row.project_name][year]) {
                r[row.company_name][row.project_name][year] = {};
                r[row.company_name][row.project_name][year].total_hours = 0;
            }

            if (!r[row.company_name][row.project_name][year][month]) {
                r[row.company_name][row.project_name][year][month] = {};
                r[row.company_name][row.project_name][year][month].total_hours = 0;

            }


            // Task 
            if (!r[row.company_name][row.project_name][year][month][row.parent_name]) {
                r[row.company_name][row.project_name][year][month][row.parent_name] = {};
                r[row.company_name][row.project_name][year][month][row.parent_name].total_hours = 0;
            }

            // Aggregation time
            total = total + row.billable_hours;
            r[row.company_name].total_hours = r[row.company_name].total_hours + row.billable_hours;
            r[row.company_name][row.project_name][year].total_hours = r[row.company_name][row.project_name][year].total_hours + row.billable_hours;
            r[row.company_name][row.project_name][year][month].total_hours = r[row.company_name][row.project_name][year][month].total_hours + row.billable_hours;
            r[row.company_name][row.project_name][year][month][row.parent_name].total_hours = r[row.company_name][row.project_name][year][month][row.parent_name].total_hours + row.billable_hours;

            if (!month_hours[month]) {
                month_hours[month] = 0;
            }
            month_hours[month] = month_hours[month] + row.billable_hours;
        }

        for (var month_index in month_hours) {
            ax.push({ "month": month_index, "hours": month_hours[month_index] });
        }

        // Render 
        var $table = $('#report_body');
        // Row Types
        var $row = function (company, project, year, month, ticket, hours) {
            var $r = $('<tr></tr>');
            $r.append("<td>" + company + "</td>");
            $r.append("<td>" + project + "</td>");
            $r.append("<td>" + year + "</td>");
            $r.append("<td>" + month + "</td>");
            $r.append("<td>" + ticket + "</td>");
            $r.append("<td><strong>" + numeral(hours).format("0,0.00") + "</strong></td>");
            return $r;
        }

        var $year_row = function (company, project, year, month, ticket, hours) {
            var $r = $('<tr class="success year_row"></tr>');
            $r.append("<td>" + company + "</td>");
            $r.append("<td>" + project + "</td>");
            $r.append("<td>" + year + "</td>");
            $r.append("<td>" + month + "</td>");
            $r.append("<td>" + ticket + "</td>");
            $r.append("<td>" + numeral(hours).format("0,0.00") + "</td>");
            return $r;
        }

        var $month_row = function (company, project, year, month, ticket, hours) {
            var $r = $('<tr class="warning"></tr>');
            $r.append("<td>" + company + "</td>");
            $r.append("<td>" + project + "</td>");
            $r.append("<td>" + year + "</td>");
            $r.append("<td>" + month + "</td>");
            $r.append("<td>" + ticket + "</td>");
            var _target = year + '_' + month;
            var _toggle = $('<a href="#"><i class="glyphicon glyphicon-plus"/> ' + numeral(hours).format("0,0.00") + "</a>");
            _toggle.attr('data-toggle', 'collapse');
            _toggle.attr('data-target', '.' + _target);
            _toggle.attr('data-year-month', _target);
            _toggle.on('click', function () {
                var group = $(this).attr('data-year-month');
                console.log('toggling ' + group);
            });

            var _toggle_cell = $('<td></td>');
            // build
            _toggle_cell.append(_toggle);
            $r.append(_toggle_cell);

            return $r;
        }

        var $detail_row = function (company, project, year, month, ticket, hours) {
            var $r = $('<tr class="detail_row collapse"></tr>');
            $r.addClass(year + '_' + month);
            $r.append("<td>" + company + "</td>");
            $r.append("<td>" + project + "</td>");
            $r.append("<td>" + year + "</td>");
            $r.append("<td>" + month + "</td>");
            $r.append("<td>" + ticket + "</td>");
            $r.append("<td>" + numeral(hours).format("0,0.00") + "</td>");
            return $r;
        }

        for (var _company in r) {
            $table.append($row(_company, " ", " ", " ", " ", total));

            for (var _project in r[_company]) {

                for (var _year in r[_company][_project]) {
                    var year_data = r[_company][_project][_year];
                    $table.append($year_row(_company, _project, _year, " ", "<small class='pull-right'>YTD Total</small>", "<strong>" + year_data.total_hours + "</strong>"));

                    for (var _month in r[_company][_project][_year]) {
                        if (_month != 'total_hours') {
                            var month_data = r[_company][_project][_year][_month];
                            $table.append($month_row(_company, _project, _year, _month, "<small class='pull-right'>Monthly Total</small> ", month_data.total_hours));

                            for (var _ticket in r[_company][_project][_year][_month]) {
                                if (_ticket != 'total_hours') {

                                    var ticket_data = r[_company][_project][_year][_month][_ticket];
                                    $table.append($detail_row("", "", _year, _month, _ticket, ticket_data.total_hours));
                                }
                            }
                        }
                    }
                }
            }
        }

        // Init DataTables + Table Tools 
        $('#report_table').DataTable({
            dom: 'T<"clear">lfrtip',
            paging: false,
            searching: false,
            ordering: false,
            TableTools: {
                "sSwfPath": "swf/copy_csv_xls.swf",
                "sSwfPath": "http://localhost:21775/Content/swf/copy_csv_xls_pdf.swf"
            }
        });

        // Render Chart 
        var svg = dimple.newSvg("#analytics-area", 350, 400);
        var myChart = new dimple.chart(svg, ax);
        myChart.setBounds("10%,10", "10%,1", 320, 320);
        var x = myChart.addCategoryAxis("x", "month");
        x.lineMarkers = true;
        x.addOrderRule(["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]);
        myChart.addMeasureAxis("y", "hours");
        var s = myChart.addSeries(null, dimple.plot.line);
        myChart.draw();


    });

});