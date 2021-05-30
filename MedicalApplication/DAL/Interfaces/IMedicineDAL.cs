namespace MedicalApplication.DAL.Interfaces
{
    public interface IMedicineDAL
    {
        /// <summary>
        /// get the list of medicine from the file present at path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        string GetMedicines(string filePath);

        /// <summary>
        /// adds the medicine details
        /// </summary>
        /// <param name="medicineList"></param>
        /// <param name="filePath"> saves the medicine data to the file present at this path</param>
        /// <returns></returns>
        bool AddMedicine(string medicineList, string filePath);
    }
}
