using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IPersonaDireccionRepository<PersonaDireccionEntity>
    {
        Task<IEnumerable<PersonaDireccionEntity>> GetEntityByPersonIdAsync(Guid PacienteId);
    }
}

