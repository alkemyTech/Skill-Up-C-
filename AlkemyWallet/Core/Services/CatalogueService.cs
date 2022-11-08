using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using static challenge.Services.ImageService;

namespace AlkemyWallet.Core.Services
{
    public class CatalogueService :ICatalogueService
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public CatalogueService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;

        }
        public async Task<Catalogue> GetCatalogueById(int id)
        {
            var catalogue = await _unitOfWork.CatalogueRepository.GetById(id);            
            return catalogue;
        }

        public async Task<IEnumerable<Catalogue>> GetCatalogues()
        {
            var catalogues = await _unitOfWork.CatalogueRepository.GetAll();
            catalogues = catalogues.OrderBy(x => x.Points);
            return catalogues;
        }

        public async Task InsertCatalogue(CatalogueForCreationDTO catalogueDTO)
        {
            string path = "";

            if (catalogueDTO.ImageFile is not null)
                path = await _imageService.StoreImage(catalogueDTO.ImageFile, ImageType.Catalogue);           

            var catalogue = _mapper.Map<Catalogue>(catalogueDTO);
            catalogue.Image = path;

            await _unitOfWork.CatalogueRepository.Insert(catalogue);
        }
        public async Task<bool> DeleteCatalogue(int id)
        {

            return await _unitOfWork.CatalogueRepository.Delete(id);

        }

        public async Task<bool> UpdateCatalogues(int id, CatalogueForUpdateDTO CatalogueDTO)
        {
            var catalogueEntity = await _unitOfWork.CatalogueRepository.GetById(id);

            if (catalogueEntity is null)
                return false;

            if (CatalogueDTO.Points is not null)
            {
                catalogueEntity.Points = CatalogueDTO.Points.Value;
            }

            if (CatalogueDTO.ImageFile is not null)
            {
                var path = await _imageService.StoreImage(CatalogueDTO.ImageFile, ImageType.Catalogue);

                catalogueEntity.Image = path;
            }

            if (CatalogueDTO.Product_description is not null)
            {
                catalogueEntity.Product_description = CatalogueDTO.Product_description;
            }


            return await _unitOfWork.CatalogueRepository.Update(catalogueEntity);

        }

    }
}
