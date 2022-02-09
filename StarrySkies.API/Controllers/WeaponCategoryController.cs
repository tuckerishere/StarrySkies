using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
using StarrySkies.Services.ResponseModels;
using StarrySkies.Services.Services.WeaponCategories;

namespace StarrySkies.API.Controllers
{
    [ApiController]
    [Route("/api/weaponCategory")]
    public class WeaponCategoryController : ControllerBase
    {
        private readonly IWeaponCategoryService _categoryService;

        public WeaponCategoryController(IWeaponCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<List<WeaponCategoryResponseDto>>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<List<WeaponCategoryResponseDto>>> GetAllWeaponCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ServiceResponse<List<WeaponCategoryResponseDto>> weaponCategories = new ServiceResponse<List<WeaponCategoryResponseDto>>();
            weaponCategories.Data = _categoryService.GetWeaponCategories().Data.ToList();
            return Ok(weaponCategories);
        }

        [HttpGet("{id}", Name ="GetWeaponCategory")]
        [ProducesResponseType(200, Type = typeof(WeaponCategoryResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<WeaponCategoryResponseDto>> GetWeaponCategory(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            ServiceResponse<WeaponCategoryResponseDto> categoryToReturn = _categoryService.GetWeaponCategoryById(id);

            if(categoryToReturn.Data == null)
            {
                return NotFound(categoryToReturn);
            }

            return Ok(categoryToReturn);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<WeaponCategoryResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<WeaponCategoryResponseDto>> DeleteWeaponCategory(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            ServiceResponse<WeaponCategoryResponseDto> categoryToDelete = _categoryService.DeleteWeaponCategory(id);

            if(!categoryToDelete.Success)
            {
                return NotFound(categoryToDelete);
            }

            return Ok(categoryToDelete);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ServiceResponse<WeaponCategoryResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<CreateWeaponCategoryDto>> CreateWewaponCategory(CreateWeaponCategoryDto createWeaponCategory)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            ServiceResponse<WeaponCategoryResponseDto> createdCategory = _categoryService.CreateWeaponCategory(createWeaponCategory);

            if(!createdCategory.Success)
            {
                return BadRequest(createdCategory);
            }

            return CreatedAtAction(nameof(GetWeaponCategory), new { id = createdCategory.Data.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type=typeof(ServiceResponse<WeaponCategoryResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ServiceResponse<WeaponCategoryResponseDto>> UpdateWeaponCategory(int id, [FromBody]CreateWeaponCategoryDto updateWeaponCategory){
            if (updateWeaponCategory.Name == null || updateWeaponCategory.Name.Trim() == "")
            {
                return BadRequest("Please Enter Weapon Category Name");
            }

            ServiceResponse<WeaponCategoryResponseDto> updatedWeaponCategory = _categoryService.UpdateWeaponCategory(id, updateWeaponCategory);

            if(!updatedWeaponCategory.Success)
            {
                return NotFound(updatedWeaponCategory);
            }

            return Ok(updatedWeaponCategory);
        }
    }
}