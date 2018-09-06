using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player
{
	public class PlayerWeaponsController : MonoBehaviour
	{
		public Weapon[] weapons;

		[Space()]
		public Weapon currentWeapon;
		public string startingWeapon;

		private void Start()
		{
			ChangeWeapon(startingWeapon);
		}

		public void ChangeWeapon(string weaponName)
		{
			if (weaponName == "None")
			{
				DeactivateWeapons();
				return;
			}

			DeactivateWeapons();

			ActivateWeapon(weaponName);

			// Reset back to original weapon if we cant find the one we are looking for
			currentWeapon.gameObject.SetActive(true);
		}

		public void Shoot()
		{
			if (currentWeapon != null)
				currentWeapon.Shoot();
		}

		private void DeactivateWeapons()
		{
			for (int i = 0; i < weapons.Length; i++)
			{
				weapons[i].gameObject.SetActive(false);
			}
		}

		private void ActivateWeapon(string weaponName)
		{
			for (int i = 0; i < weapons.Length; i++)
			{
				if (weapons[i].weaponName.Equals(weaponName))
				{
					weapons[i].gameObject.SetActive(true);
					currentWeapon = weapons[i];
					return;
				}
			}

			print("'" + weaponName + "' not found!");
		}
	}
}
