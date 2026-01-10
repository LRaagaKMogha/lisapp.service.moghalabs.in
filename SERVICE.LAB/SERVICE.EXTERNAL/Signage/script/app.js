angular.module('app', []);

angular.module('app').controller("DisplayQueueController", function($scope,dataService){
    
     $scope.LoadTokenList = function () { 
      $scope.data = null;
      dataService.getData(function(dataResponse) {
        $scope.data = dataResponse;
		  var MenuHTML = ""; 
          var colorcode="";
           for (i = 0; i < $scope.data.length; i++) {
                     if (i === 0) {
                       colorcode="blue";
                       }
                     else if (i % 2 === 0) {
                       colorcode="blue"; 
                     }
                     else {
                       colorcode="red";
                      }
                    MenuHTML += "<li style='background-color:"+colorcode +";'><div class='row'>";
                    MenuHTML += " <div class='col-md-2 col-xs-2 divTableCell' >" + $scope.data[i].roomNo + "</div>";
                    MenuHTML += " <div class='col-md-4 col-xs-4 divTableCell' >" + $scope.data[i].physicianName + "</div>";
                    MenuHTML += " <div class='col-md-2 col-xs-2 divTableCell'>" + $scope.data[i].sno + "</div>";
                    MenuHTML += " <div class='col-md-4 col-xs-4 divTableCell'>" + $scope.data[i].patientName + "</div>";
                    MenuHTML += "</li></div>";
                }
                $("#vertical-ticker").html(MenuHTML);


                $('#vertical-ticker').totemticker({
					speed		:	4000,
					interval	:	100,
                    row_height: '100px',
                    next: '#ticker-next',
                    previous: '#ticker-previous',
                    stop: '#stop',
                    start: '#start',
                    mousestop: true,
                }); 
      });	 

     }

});
angular.module('app').service('dataService', function($http) {
    delete $http.defaults.headers.common['X-Requested-With'];
    this.getData = function(callbackFunc) {
        $http({
            method: 'GET',
            url: 'http://uat.yungtrepreneur.com/service/api/display/GetDisplayView?VenueNo=1&VenueBranchNo=1',
        }).success(function(data){
            callbackFunc(data);
        }).error(function(){
            alert("error");
        });
     }
});