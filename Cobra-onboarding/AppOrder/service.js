appOrder.service("orderService", function ($http, $q) {
    this.GetOrders = function () {
        return $http.get("/Order/OrderList");
    };
    this.GetCustomers = function () {
        return $http.get("/Order/CustomerList");
    };
    this.GetProducts = function () {
        return $http.get("/Order/ProductList");
    };

    this.GetPrice = function (pId) {
        return $http.get("/Order/ProductList");
    }

    this.DeleteOrder = function (orderId) {
        var response = $http({
            method: "post",
            url: "/Order/DeleteOrder",
            params: {
                orderId: JSON.stringify(orderId)
            }
        });
        return response;
    }

    this.UpdateOrder = function (order) {
        var response = $http({
            method: "post",
            url: "/Order/SaveOrder",
            data: JSON.stringify(order),
            dataType: "json"
        });
        return response;
    }

    this.AddOrder = function (order) {
        var response = $http({
            method: "post",
            url: "/Order/SaveOrder",
            data: JSON.stringify(order),
            dataType: "json"
        });
        return response;
    }



});