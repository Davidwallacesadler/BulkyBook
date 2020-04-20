var dataTable;

// Wait until the document is ready and then run this function
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // Here we want to use dataTables.net -- we will use the table id from our view html
    // 1. we want to set the dataTable variable to our table in our view
    dataTable = $('#tblData').dataTable({
        // 2. Here we will be using an ajax call to load the table
        "ajax": {
            // 3. Specify the url we want to load - which is the Admin area, Category controller, GetAll action:
            "url" : "/Admin/Category/GetAll"
        },
        // 4. Now we want to specify the columns of the table, that being the name and the edit/delete buttons
        "columns": [
            // NOTE: camel casing not pascal casing here for the property name
            { "data": "name", "width": "60%" },
            {
                // Here we have the id that we are passing into our render method
                "data": "id",
                "render": function (data) {
                    // NOTE: when returning HTML we need to use the lowercase tilda to specify this:
                    return `
                            <div class="text-center">
                                <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                     <i class="fas fa-edit"></i>
                                 </a>
                                 <a class="btn btn-danger text-white" style="cursor:pointer">
                                     <i class="fas fa-trash-alt"></i>
                                 </a>
                            </div>
                          `;
                }, "width":"40%"

            }
        ]
    });
}