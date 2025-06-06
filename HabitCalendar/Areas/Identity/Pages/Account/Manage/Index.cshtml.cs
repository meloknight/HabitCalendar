﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using HabitCalendar.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HabitCalendar.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel:PageModel
    {
        protected readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager )
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display( Name = "Phone number" )]
            public string PhoneNumber { get; set; }

            // Addition for custom UI on Registration screen and storage of Display Username
            [Required]
            [Display( Name = "Username" )]
            [MaxLength( 30, ErrorMessage = "Max length is 30 characters" )]
            [RegularExpression( @"^[^\[\]\(\)\{\}]+$", ErrorMessage = "Special characters, such as brackets, are not allowed." )]
            public string DisplayUserName { get; set; }
        }

        private async Task LoadAsync( IdentityUser user )
        {
            var userName = await _userManager.GetUserNameAsync( user );
            var phoneNumber = await _userManager.GetPhoneNumberAsync( user );
            var userId = await _userManager.GetUserIdAsync( user );
            var displayUserName = _db.ApplicationUsers
                .Where( d => d.Id == userId )
                .Select( d => d.DisplayUserName )
                .FirstOrDefault();

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                DisplayUserName = displayUserName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync( User );

            if ( user == null )
            {
                return NotFound( $"Unable to load user with ID '{_userManager.GetUserId( User )}'." );
            }

            await LoadAsync( user );
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync( User );
            if ( user == null )
            {
                return NotFound( $"Unable to load user with ID '{_userManager.GetUserId( User )}'." );
            }

            if ( !ModelState.IsValid )
            {
                await LoadAsync( user );
                return Page();
            }

            //~~~~~~~~~~~~~~~~~~~~~~~~
            var userId = await _userManager.GetUserIdAsync( user );
            var currentDisplayUserName = _db.ApplicationUsers
                .Where( c => c.Id == userId )
                .Select( c => c.DisplayUserName )
                .FirstOrDefault();

            if ( currentDisplayUserName != Input.DisplayUserName )
            {
                var currentApplicationUser = await _db.ApplicationUsers.FindAsync( userId );
                currentApplicationUser.DisplayUserName = Input.DisplayUserName;
                await _db.SaveChangesAsync();
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~



            var phoneNumber = await _userManager.GetPhoneNumberAsync( user );
            if ( Input.PhoneNumber != phoneNumber )
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync( user, Input.PhoneNumber );
                if ( !setPhoneResult.Succeeded )
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync( user );
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
