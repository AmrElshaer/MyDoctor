using MYDoctor.Core.Domain.Common;
namespace MYDoctor.Core.Domain.Entities
{
    public class DiseaseMedicin:BaseEntity
    {
        
        public int DiseaseId { get; set; }
        public int MedicinId { get; set; }
        public Disease Disease { get; set; }
        public Medicin Medicin { get; set; }
    }
}
