var _companies = new Array();

var Address = function (addr) {
    this.StreetLine1 = addr.StreetLine1;
    this.StreetLine2 = addr.StreetLine2;
    this.City = addr.City;
    this.State = addr.State;
    this.ZipCode = addr.ZipCode;
};


var Company = function (company) {

    this.Id = company.Id;
    this.Name = company.Name
    this.Address = new Address(company.Address);

};

var buildCompanyDisplay = function (comp) {

    if (!(comp instanceof Company)) { throw ("comp is not an instance of Company"); }

    var tr = document.createElement("tr");

    tr.myCompany = comp;

    var name = document.createElement("td");
    var buttons = document.createElement("td");

    tr.appendChild(name);
    tr.appendChild(buttons);

    name.appendChild(document.createTextNode(comp.Name));

    var editLink = document.createElement('a');
    editLink.setAttribute("href", "#");
    var el = document.createElement('img');
    el.setAttribute('src', '/content/images/edit2.png');
    el.setAttribute('alt', 'edit');
    editLink.appendChild(el);
    $(editLink).addClass("edit-button");

    $(editLink).click(function () {
        var myParent = $(this).parent().parent()[0];
        showCompanyForm(myParent.myCompany);
    });

    var deleteLink = document.createElement('a');
    deleteLink.setAttribute("href", "#");
    var el = document.createElement('img');
    el.setAttribute('src', '/content/images/delete.png');
    el.setAttribute('alt', 'delete');
    deleteLink.appendChild(el);
    $(deleteLink).addClass("delete-button");
    

    $(deleteLink).click(function () {
        var myParent = $(this).parent().parent()[0];
        $.ajax({
            url: removeCompanyUrl,
            data: { Id: myParent.myCompany.Id },
            success: removeFromDisplay,
            dataType: "json",
            type: "DELETE"
        });


    });

    buttons.appendChild(editLink);
    buttons.appendChild(deleteLink);

    function removeFromDisplay(response) {
        if (response.Success) {
            $(tr).remove();
        }
        else {
            $.jGrowl('You cannot remove this company', { theme: 'jgrowl-error' });
        }
    }

    return tr;
};