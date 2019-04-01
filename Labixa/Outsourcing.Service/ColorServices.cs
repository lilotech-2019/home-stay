using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Service
{

    public interface IColorService
    {

        IEnumerable<Color> GetColors();
        Color GetColorById(int ColorId);
        void CreateColor(Color Color);
        void EditColor(Color ColorToEdit);
        void DeleteColor(int ColorId);
        void SaveColor();
        IEnumerable<ValidationResult> CanAddColor(Color Color);

    }
    public class ColorService : IColorService
    {
        #region Field
        private readonly IColorRepository ColorRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ColorService(IColorRepository ColorRepository, IUnitOfWork unitOfWork)
        {
            this.ColorRepository = ColorRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Color> GetColors()
        {
            var Colors = ColorRepository.GetAll();
            return Colors;
        }

        public Color GetColorById(int ColorId)
        {
            var Color = ColorRepository.GetById(ColorId);
            return Color;
        }

        public void CreateColor(Color Color)
        {
            ColorRepository.Add(Color);
            SaveColor();
        }

        public void EditColor(Color ColorToEdit)
        {
            ColorRepository.Update(ColorToEdit);
            SaveColor();
        }

        public void DeleteColor(int ColorId)
        {
            //Get Color by id.
            var Color = ColorRepository.GetById(ColorId);
            if (Color != null)
            {
                ColorRepository.Delete(Color);
                SaveColor();
            }
        }

        public void SaveColor()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddColor(Color Color)
        {

            //    yield return new ValidationResult("Color", "ErrorString");
            return null;
        }

        #endregion
    }
}
