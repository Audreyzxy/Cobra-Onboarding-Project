app.service("crudService", function ($http,$q) {
    //get All Books
    this.GetCustomers = function () {
        return $http.get("/Customer/CustomerList");
    };
   
    this.GetCustomer = function (customerId) {
        var response = $http({
            method: "post",
            url: "/Customer/GetCustomerById",
            params: {
                customerId: JSON.stringify(customerId)
            }
        });
        return response;
    }

    //this.SaveCustomer = function (customer) {
    //    var response = $http({
    //        method: "post",
    //        url: "/Customer/SaveCustomer",
    //        data: JSON.stringify(customer),
    //        dataType: "json"
    //    });
    //    return response;
    //}

    this.DeleteCustomer = function (customerId) {
        var response = $http({
            method: "post",
            url: "/Customer/DeleteCustomer",
            params: {
                customerId: JSON.stringify(customerId)
            }
        });
        return response;
    }

    this.UpdateCustomer = function (customer) {
        var response = $http({
            method: "post",
            url: "/Customer/SaveCustomer",
            data: JSON.stringify(customer),
            dataType: "json"            
        });
        return response;
    }

    this.AddCustomer = function (customer) {
        var response = $http({
            method: "post",
            url: "/Customer/SaveCustomer",
            data: JSON.stringify(customer),
            dataType: "json"
        });
        return response;
    }

   
    
});