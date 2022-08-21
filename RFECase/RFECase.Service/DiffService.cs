using RFECase.Domain.DTO;
using RFECase.Domain.DTO.Base;
using RFECase.Domain.Entities;
using RFECase.Repository.Abstract;
using RFECase.Service.Abstract;
using RFECase.Service.Utility;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RFECase.Service
{
    public class DiffService : IDiffService
    {
        private IDiffRepository<DiffEntity> _repository;

        public DiffService(IDiffRepository<DiffEntity> repository)
        {
            _repository = repository;
        }
        public async Task<DiffResponseDTO> GetDiff(int id)
        {
            var existedEntity = await _repository.Get(id);
            if (existedEntity == null)
            {
                return new DiffResponseDTO
                {
                    StatusCode = HttpStatusCode.NotFound.GetHashCode(),
                    Result = $"Record {id} could not be found !",
                    Elaborations = null,
                    Detail = null
                };
            }

            return await GetDiffInternal(existedEntity);
        }

        public async Task<BaseResponseDTO> SendToLeft(int id, string input)
        {
            var entity = await _repository.Get(id);
         
            bool toBeAdded = (entity == null);

            if (entity == null)
            {
                entity = new DiffEntity { Id = id, LeftExpression = input };
            }
            else
            {
                entity.LeftExpression = input;
            }

            return await SendInternal(entity, toBeAdded);

        }

        public async Task<BaseResponseDTO> SendToRight(int id, string input)
        {
            var entity = await _repository.Get(id);
            bool toBeAdded = (entity == null);

            if (entity == null)
            {
                entity = new DiffEntity { Id = id, RightExpression = input };
            }
            else
            {
                entity.RightExpression = input;
            }

            return await SendInternal(entity, toBeAdded);
        }

        #region Internal

        private async Task<BaseResponseDTO> SendInternal(DiffEntity entity, bool toBeAdded)
        {
            if (toBeAdded)
            {
                await _repository.Add(entity);
            }
            else
            {
                entity.ModifiedDate = DateTime.Now;
                await _repository.Update(entity);
            }

            return new BaseResponseDTO
            {
                StatusCode = HttpStatusCode.Accepted.GetHashCode(),
                Result = HttpStatusCode.Accepted.ToString()
            };
        }


        private Task<DiffResponseDTO> GetDiffInternal(DiffEntity existedEntity)
        {
            var diffResponse = new DiffResponseDTO();

            if (existedEntity.EqualityStatus != Domain.EqualityStatus.AreNotEqualInContent)
            {
                diffResponse.StatusCode = HttpStatusCode.OK.GetHashCode();
                diffResponse.Result = existedEntity.EqualityStatus.ConvertToStringFrom();

                return Task.FromResult(diffResponse);
            }

            diffResponse.StatusCode = HttpStatusCode.OK.GetHashCode();
            diffResponse.Result = existedEntity.EqualityStatus.ConvertToStringFrom();

            var stringLength = existedEntity.LeftExpression.Length;
            for (int i = 0; i < stringLength; i++)
            {
                char leftChar = existedEntity.LeftExpression[i];
                char rightChar = existedEntity.RightExpression[i];
                if (leftChar != rightChar)
                {
                    diffResponse.Elaborations.Add(new DiffResponseElaborationDTO
                    {
                        DiffIndex = i + 1,
                        LeftDiffValue = leftChar,
                        RightDiffValue = rightChar
                    });
                }
            }

            diffResponse.Detail = new DiffResponseDetailDTO
            {
                DiffID = existedEntity.Id,
                LeftExpression = existedEntity.LeftExpression,
                LeftExpressionLength = existedEntity.LeftExpression.Length,
                RightExpression = existedEntity.RightExpression,
                RightExpressionLength = existedEntity.RightExpression.Length
            };

            return Task.FromResult(diffResponse);
        }

        #endregion
    }
}
