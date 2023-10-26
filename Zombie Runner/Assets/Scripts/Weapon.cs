using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float range = 100;
    [SerializeField] float damage = 30f;
    [SerializeField] float timeBetweenShots = 2f;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioSource fireAudio;
    [SerializeField] AudioClip audioClip;
    [SerializeField] bool canPlay = false;
    bool canShoot = true;
    bool isShooting = false;

    void OnEnable()
    {
        canShoot = true;
    }

    void Start()
    {
        fireAudio = GetComponent<AudioSource>();
        fireAudio.clip = audioClip;
    }

    void Update()
    {
        DisplayAmmo();
        if(canPlay)
        {
            if(Input.GetMouseButtonDown(0) && canShoot)
            {
                PlayFullClip();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0) && canShoot)
            {
                isShooting = true;
                StartCoroutine(Shoot());
            }
            if(Input.GetMouseButtonUp(0))
            {
                isShooting = false;
                StopShootingSound();
            }
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    private void PlayFullClip()
    {
        if(ammoSlot.GetCurrentAmmo(ammoType)>0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);

            fireAudio.PlayOneShot(audioClip);
            canShoot = false;
            StartCoroutine(ResetCanShoot());
        }
    }

    IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(isShooting && ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            if(!fireAudio.isPlaying)
            {
                fireAudio.Play();
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
        StopShootingSound();
        canShoot = true;
    }

    void StopShootingSound()
    {
        if(fireAudio.isPlaying && (fireAudio.clip == audioClip))
        {
            fireAudio.Stop();
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(damage);
        }
        else { return; }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
    }
}