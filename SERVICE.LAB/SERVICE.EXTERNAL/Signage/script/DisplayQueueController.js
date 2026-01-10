define(['app'], function (app) {
    app.controller('DisplayQueueController', function ($scope, $http, $window, $rootScope, $q, CommonHttpService, utility, TranslationService, CommonErrorService, $location, QueryString) {
       
        var url = $location.absUrl().split('?')[0];      
        if (url.toLowerCase().indexOf("displayqueue") >= 0) {
            var Para = atob($location.absUrl().split('?')[1])
            var Parameters = Para.split("&");
            $scope.TenentID = Parameters[0];
            $scope.TenantBranchID = Parameters[1];          
        }
        else {
            alert("Invalid URL");
        }      
        $scope.LoadTokenList = function () {         
            CommonHttpService.GetService("RegistrationNew/GetAppointmentlistToday?TenantNo=" + $scope.TenentID + "&TenantBranchNo=" + $scope.TenantBranchID).then(function (response) {
                $scope.data = angular.fromJson(response);              
                var MenuHTML = "";               
                for (i = 0; i < $scope.data.length; i++) {
                    MenuHTML += "<li><div class='row'>";
                    MenuHTML += " <div class='col-md-3 col-xs-6 divTableCell' >" + $scope.data[i].ResourceName + "</div>";
                    MenuHTML += " <div class='col-md-3 col-xs-6 divTableCell' >" + $scope.data[i].Name + "</div>";
                    MenuHTML += " <div class='col-md-2 col-xs-4 divTableCell'>" + $scope.data[i].AppointmentSlotNo + "</div>";
                    MenuHTML += " <div class='col-md-2 col-xs-4 divTableCell'>" + $scope.data[i].AppointmentStartDate + "</div>";
                    MenuHTML += " <div class='col-md-2 col-xs-4 divTableCell'>" + $scope.data[i].AppointmentStatus + "</div>";
                    MenuHTML += "</li></div>";
                }
                $("#vertical-ticker").html(MenuHTML);
                
            });
        }
    });
});