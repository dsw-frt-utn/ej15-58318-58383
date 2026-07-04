using Dsw2026Ej15.Data.Sources;
using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly AppDbContext _context;

        public PersistenceEf(AppDbContext context)
        {
            _context = context;
        }

        public List<Doctor> GetDoctors()
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .AsNoTracking()
                .ToList();
        }

        public List<Speciality> GetSpecialities()
        {
            return _context.Specialities
                .AsNoTracking()
                .ToList();
        }

        public Doctor AddDoctor(Doctor doctor)
{
    _context.Specialities.Attach(doctor.Speciality);
    _context.Doctors.Add(doctor);
    _context.SaveChanges();

    return doctor;
}

        public Doctor? GetDoctorById(Guid id)
        {
            return _context.Doctors
                .Include(d => d.Speciality)
                .FirstOrDefault(d => d.Id == id);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            var existing = _context.Doctors.FirstOrDefault(d => d.Id == doctor.Id);

            if (existing is null)
            {
                return;
            }

            _context.Entry(existing).CurrentValues.SetValues(doctor);
            _context.SaveChanges();
        }
    }
}
