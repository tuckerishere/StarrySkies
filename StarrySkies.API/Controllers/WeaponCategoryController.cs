using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
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
        [ProducesResponseType(200, Type = typeof(List<WeaponCategoryResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<List<WeaponCategoryResponseDto>> GetAllWeaponCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            List<WeaponCategoryResponseDto> weaponCategories = _categoryService.GetWeaponCategories().ToList();
            return Ok(weaponCategories);
        }

        [HttpGet("{id}", Name ="GetWeaponCategory")]
        [ProducesResponseType(200, Type = typeof(WeaponCategoryResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<WeaponCategoryResponseDto> GetWeaponCategory(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            WeaponCategoryResponseDto categoryToReturn = _categoryService.GetWeaponCategoryById(id);

            if(categoryToReturn == null || categoryToReturn.Id == 0)
            {
                return NotFound();
            }

            return Ok(categoryToReturn);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(WeaponCategoryResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<WeaponCategoryResponseDto> DeleteWeaponCategory(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            WeaponCategoryResponseDto categoryToDelete = _categoryService.DeleteWeaponCategory(id);

            if(categoryToDelete == null || categoryToDelete.Id == 0)
            {
                return NotFound();
            }

            return Ok(categoryToDelete);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(WeaponCategoryResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<CreateWeaponCategoryDto> CreateWewaponCategory(CreateWeaponCategoryDto createWeaponCategory)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            WeaponCategoryResponseDto createdCategory = _categoryService.CreateWeaponCategory(createWeaponCategory);

            if(createdCategory.Name == null)
            {
                return BadRequest("Please enter Weapon Category Name.");
            }

            return CreatedAtAction(nameof(GetWeaponCategory), new { id = createdCategory.Id }, createdCategory);
        }
    }
}