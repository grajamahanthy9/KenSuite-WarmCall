var app = angular.module('myApp', ['ui.calendar']);
app.controller('myNgController', ['$scope', '$http', 'uiCalendarConfig', function ($scope, $http, uiCalendarConfig) {
    
    $scope.SelectedEvent = null;
    var isFirstTime = true;
 
    $scope.events = [];
    $scope.eventSources = [$scope.events];
 
 
    //Load events from server
    //$http.get('/home/getevents', {
    //    cache: true,
    //    params: {}
    //}).then(function (data) {
    //    $scope.events.slice(0, $scope.events.length);
    //    angular.forEach(data.data, function (value) {
    //        $scope.events.push({
    //            title: value.Title,
    //            description: value.Description,
    //            start: new Date(parseInt(value.StartAt.substr(6))),
    //            end: new Date(parseInt(value.EndAt.substr(6))),
    //            allDay : value.IsFullDay,
    //            stick: true
    //        });
    //    });
    //});
    $scope.events = [
         { title: 'All Day Event', start: new Date(2018, 16, 2) },
         
    ];
    //configure calendar
    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            displayEventTime: false,
            header: {
                left: 'month basicWeek basicDay agendaWeek agendaDay',
                center: 'title',
                right:'today prev,next'
            },
            eventClick: function (event) {
                $scope.SelectedEvent = event;
            },
            eventAfterAllRender: function () {
                if ($scope.events.length > 0 && isFirstTime) {
                    //Focus first event
                    //uiCalendarConfig.calendars.myCalendar.fullCalendar('gotoDate', $scope.events[0].start);

                    setTimeout(function () {
                        uiCalendarConfig.calendars.myCalendar.fullCalendar('gotoDate', $scope.events[0].start);
                    }, 200);

                    isFirstTime = false;
                }
            }
        }
    };

    $scope.schedule = {};


    if ($("#UserAvailabilityJsonString").val() != "")
        $scope.schedules = JSON.parse($("#UserAvailabilityJsonString").val());
    else
        $scope.schedules = [];
    
        $scope.errExist = false;
    $scope.errMsg = "";
    $scope.updateScheduleScope = function () {
        $scope.schedule = {
            scheduleDate: $("#scheduleDate").val(), startTime: $("#startTime").val(),
            endTime: $("#endTime").val(), untilDate: $("#untilDate").val(),
            repeatSchedule: $("#repeatSchedule")[0].checked
        };
    }

    $scope.addAvailability = function () {
        $scope.updateScheduleScope();

        if ($scope.validateScheduleData() == true) {
            $scope.schedules.push($scope.schedule);
            $scope.schedule = {};
        }
        $("#UserAvailabilityJsonString").val(JSON.stringify($scope.schedules));
    }

    $scope.deleteAvailability = function (sc) {
        var idx = $scope.schedules.indexOf(sc);
        $scope.schedules.splice(idx, 1);
        $("#UserAvailabilityJsonString").val(JSON.stringify($scope.schedules));
    };

    $scope.validateScheduleData = function () {
        $scope.updateScheduleScope();        
                                
        if (!IsNullOrEmptyOrUndefined($scope.schedule.scheduleDate) && !IsNullOrEmptyOrUndefined($scope.schedule.startTime) && !IsNullOrEmptyOrUndefined($scope.schedule.endTime)) {
            if ($scope.schedule.repeatSchedule) {
                if (!IsNullOrEmptyOrUndefined($scope.schedule.untilDate)) {
                    if (moment($scope.schedule.scheduleDate, "MM/DD/YYYY") >= moment($scope.schedule.untilDate, "MM/DD/YYYY")) {
                        $scope.errExist = true;
                        $scope.errMsg = "Until Date should be greater than Schedule Date.";
                        $scope.successExist = false;
                        return false;
                    }
                    else {
                        $scope.errExist = false;
                        $scope.errMsg = "";
                        $scope.successExist = true;
                        return true;
                    }
                }
                else {
                    $scope.errExist = true;
                    $scope.errMsg = "Schedule Date, Start Time, End Time and Until Date are required.";
                    $scope.successExist = false;
                    return false;
                }
            }
            else {
                
                if (moment($scope.schedule.startTime, 'LT') >= moment($scope.schedule.endTime, 'LT')) {
                    $scope.errExist = true;
                    $scope.errMsg = "End Time should be greater than End Time.";
                    $scope.successExist = false;
                    return false;
                }
                else {
                    $scope.errExist = false;
                    $scope.errMsg = "";
                    $scope.successExist = true;
                    return true;
                }
            }
        }
        else {
            $scope.errExist = true;
            $scope.errMsg = "Schedule Date, Start Time and End Time are required."
            $scope.successExist = false;
            return false;
        }
        
    }
}])


$('#startdatepicker').datetimepicker({ format: 'MM/DD/YYYY' }).keydown(false);
$('#starttimepicker').datetimepicker({ format: 'LT' }).keydown(false);
$('#endtimepicker').datetimepicker({ format: 'LT' }).keydown(false);
$('#enddatepicker').datetimepicker({ format: 'MM/DD/YYYY' }).keydown(false);

function IsNullOrEmptyOrUndefined(ele) {
    if (ele == null || ele == "" || typeof (ele) == "undefined")
        return true;
    else
        return false;
}


setPrice();
function setPrice() {
    var roleLev = $('#DesignationRoleLevel').val();
    
    if (!IsNullOrEmptyOrUndefined(roleLev)) {
        for (var i = 0; i < priceList.length; i++) {
            if (priceList[i].Id == roleLev) {
                $("#idPrice").html(priceList[i].Price);
                $("#Price").val(priceList[i].Price);
            }
        }
    }
}

//$(function () {
//    $('.form_datetime').datetimepicker();
//});

//$(function () {
//    $(".form_datetimeduration").datetimepicker();
//});

//$(function () {
//    $('#datetimepicker3').datepickerdatepickerdatepicker({
//        format: 'LT'
//    });
//});