using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IProductTypeRepository
    {
        List<TblProductType> Getproducttypemaster(ProductTypeMasterRequest protypRequest);
        ProductTypeMasterResponse Insertproducttypemaster(TblProductType tblProtyp);
        List<TblProductCategory> GetProductCategory(ProductcategoryRequest ProductcategoryRequest);
        ProductcategoryResponse InsertproductCategory(TblProductCategory TblProductCategory);
    }
}
