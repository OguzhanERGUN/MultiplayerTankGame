using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLauncher : NetworkBehaviour
{
	[Header("References")]
	[SerializeField] private Transform projectileSpawnPoint;
	[SerializeField] private InputReader inputReader;
	[SerializeField] private GameObject serverProjectilePrefab;
	[SerializeField] private GameObject clientProjectilePrefab;
	[Header("Settings")]
	[SerializeField] private float projectileSpeed;
	[SerializeField] private bool shouldFire;

	public override void OnNetworkSpawn()
	{
		if (!IsOwner) return;

		inputReader.primaryFireEvent += HandlePrimaryFire;
	}

	public override void OnNetworkDespawn()
	{
		if (!IsOwner) return;

		inputReader.primaryFireEvent += HandlePrimaryFire;

	}

	private void Update()
	{
		if (!IsOwner) return;
		if (!shouldFire) return;

		PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.up);
		SpawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);

	}


	[ServerRpc]
	private void PrimaryFireServerRpc(Vector3 position, Vector3 direction)
	{
		GameObject projectileInstance = Instantiate(
			serverProjectilePrefab,
			position,
			 Quaternion.identity);

		projectileInstance.transform.up = direction;

		SpawnDummyProjectileClientRpc(position, direction);
	}

	[ClientRpc]
	private void SpawnDummyProjectileClientRpc(Vector3 position, Vector3 direction)
	{
		if (IsOwner) return;

		SpawnDummyProjectile(position, direction);
	}

	private void SpawnDummyProjectile(Vector3 position, Vector3 direction)
	{
		GameObject projectileInstance = Instantiate(
			clientProjectilePrefab, 
			position, 
			 Quaternion.identity);

		projectileInstance.transform.up = direction;
	}

	private void HandlePrimaryFire(bool shouldFire)
	{
		this.shouldFire = shouldFire;
	}
}
