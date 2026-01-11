using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;

namespace Dev.IRepository
{
    public interface IProductTypeRepository
    {

        List<TblProductType> Getproducttypemaster(ProductTypeMasterRequest protypRequest);
        ProductTypeMasterResponse Insertproducttypemaster(TblProductType tblProtyp);

        List<TblProductCategory> GetProductCategory(ProductcategoryRequest ProductcategoryRequest);
        ProductcategoryResponse InsertproductCategory(TblProductCategory TblProductCategory);
    }

}
