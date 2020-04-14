namespace Database.Abstraction.ClinicalDocument.Common
{
    /// <summary>
    /// Generic Unit Of Work Interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// commit changes to database
        /// </summary>
        void Commit();
    }
}
