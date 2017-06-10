app.controller("customerCtrl", function ($scope, $modal, $log, crudService) {
    GetAllCustomers();
    function GetAllCustomers() {
        //debugger;
        var getCustomerData = crudService.GetCustomers();
        getCustomerData.then(function (customer) {
            $scope.customer = customer.data;

        }, function () {
            alert('Error in getting customer records');
        });
    }

    $scope.AddCustomer = function () {
        
        $scope.Action = "Add";
        $scope.open();
        GetAllCustomers();
       
    };

    $scope.EditCustomer = function (cus) {
        $scope.customer = cus;
        $scope.Action = "Update";
        $scope.open();
        GetAllCustomers();
        
       
    };
    $scope.DeleteCustomer = function (cus) {
        var getCustomerData = crudService.DeleteCustomer(cus.Id);
        getCustomerData.then(function (msg) {
            //alert(msg.data);
            GetAllCustomers();
        }, function () {
            alert('Error in deleting customer record');
        });
    }
    $scope.open = function () {
        var modalInstance = $modal.open({
            templateUrl: 'aeCustomer.html',
            controller: 'aeInstanceCtrl',
            // windowClass: 'center-modal',
            resolve: {
                customer: function () {
                    return $scope.customer;
                }
            }
        });

        modalInstance.result.then(function (response) {
                      
            $scope.currentCustomer = response;
            //console.log(response);
            GetAllCustomers();         
            //console.log(response.Id);
            $scope.Id = response.Id
            console.log($scope.Id);
            if ($scope.Id != null) {
                var getCustomerData = crudService.UpdateCustomer(response);
                getCustomerData.then(function (msg) {
                    //alert(msg.data);                    
                    GetAllCustomers();
                }, function () {
                    alert('Error in updating customer record');
                });
            }
            else {
                var Customer = {
                        Id: 0,
                        Name: response.Name,
                        Address1: response.Address1,
                        Address2: response.Address2,
                        City: response.City
                    };               
                console.log(Customer);
                var getCustomerData = crudService.AddCustomer(Customer);
                    getCustomerData.then(function (msg) {
                        //alert(msg.data);
                        GetAllCustomers();
                    }, function () {
                        alert('Error in adding customer record');
                    });
            }
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

   

});

app.controller('aeInstanceCtrl', function ($scope, $modalInstance, customer, crudService) {
    $scope.customer = customer;
    if (customer.Id == null) {
        $scope.headerTitle = "Create Customer";
    }
    else {
        $scope.headerTitle = "Edit Customer";
    }

    $scope.save = function () {       
        $modalInstance.close($scope.customer);
    };
    $scope.cancel = function ($value) {
        debugger;
        $modalInstance.dismiss("cancel");
    };
});



