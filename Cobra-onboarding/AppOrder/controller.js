appOrder.controller("orderCtrl", function ($scope, $modal, $log, orderService) {
    GetAllOrders();
    GetAllCusts();   
    GetAllProds();
    function GetAllOrders() {
        var getOrderData = orderService.GetOrders();
        getOrderData.then(function (response) {
            $scope.order = response.data;
        }, function () {
            alert('Error in getting order records');
        });
    }
    function GetAllCusts() {
        var getCustName = orderService.GetCustomers();
        getCustName.then(function (response) {
            $scope.custNames = response.data;
        }, function () {
            alert('Error in getting customer names records');
        });
    }
    function GetAllProds() {
        var getProdName = orderService.GetProducts();
        getProdName.then(function (response) {
            $scope.prodNames = response.data;
           // console.log($scope.prodNames);
        }, function () {
            alert('Error in getting product names records');
        });
    }
    function GetFirstPrice(pId) {
        var getPrice = orderService.GetPrice(pId);
        getPrice.then(function (response) {
            $scope.price = response.data[pId - 1];
            console.log(response.data[pId - 1]);
        }, function () {
            alert('Error in getting order records');
        });
    }
    $scope.AddOrder = function () {
        $scope.Action = "Add";
        $scope.open();
        GetAllOrders();
    };   
    $scope.EditOrder = function (o) {
        $scope.order = o;
        $scope.Action = "Update";
        $scope.open();
        GetAllOrders();
    };
    $scope.DeleteOrder = function (o) {
        console.log(o.Id);
        var delOrderData = orderService.DeleteOrder(o.Id);
        delOrderData.then(function (msg) {
            //alert(msg.data);
            GetAllOrders();
        }, function () {
            alert('Error in deleting order record');
        });
    }
    $scope.open = function () {
        GetAllCusts();
        GetAllProds();       
        var modalInstance = $modal.open({
            templateUrl: 'aeOrder.html',
            controller: 'aeInstanceCtrl',
            // windowClass: 'center-modal',
            resolve: {
                order: function () {
                    return $scope.order;                
                },
                custNames: function () {
                    return $scope.custNames;
                },
                prodNames: function () {
                    return $scope.prodNames;
                }
            }
        });

        modalInstance.result.then(function (response) {           
            GetAllOrders();      
            $scope.Id = response.Id
            if ($scope.Id != null) {
                var Order = {
                    Id: response.Id,
                    OrderId: response.OrderId,
                    OrderDate: response.OrderDate,
                    CustomerId: response.CustId,
                    ProductId: response.ProdId,
                    ProdPrice: response.ProdPrice
                };
                var updateOrderData = orderService.UpdateOrder(Order);
                updateOrderData.then(function (msg) {                 
                    GetAllOrders();
                }, function () {
                    alert('Error in updating order record');
                });
            }
            else {
                var Order = {
                    Id: 0,
                    //OrderId: 0,
                    OrderDate: response.OrderDate,
                    CustomerId: response.CustId,
                    ProductId: response.ProdId,
                    ProdPrice: response.ProdPrice
                };
                var addOrderData = orderService.AddOrder(Order);
                addOrderData.then(function (msg) {
                    GetAllOrders();
                }, function () {
                    alert('Error in adding order record');
                });
            }
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
});

appOrder.controller('aeInstanceCtrl', function ($scope, $modalInstance, order, custNames, prodNames, orderService) {
    $scope.order = order;
    $scope.custNames = custNames;
    $scope.prodNames = prodNames;
    var p = {
        Price : order.ProdPrice
    };
    $scope.price = p;
    console.log($scope.price);
    $scope.GetPrice = function (pId) {
        var getPrice = orderService.GetPrice(pId);       
        getPrice.then(function (response) {
            $scope.price = response.data[pId - 1];            
        }, function () {
            alert('Error in getting order records');
        });
    }
    if (order.Id == null) {
        $scope.headerTitle = "Create Order";
    }
    else {
        $scope.headerTitle = "Edit Order";
    }
    $scope.save = function () {        
        $modalInstance.close($scope.order);
    };
    $scope.cancel = function ($value) {
        $modalInstance.dismiss("cancel");
    };
});



