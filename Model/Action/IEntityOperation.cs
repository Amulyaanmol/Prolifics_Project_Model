

namespace Model.Action
{
    public interface IEntityOperation <ERPModel>
    {
        ActionResult Insert(ERPModel erp);
        DataResults<ERPModel> DisplayAll();
        ERPModel DisplayById(int id);
        ActionResult Delete(ERPModel erp);
    }
}
