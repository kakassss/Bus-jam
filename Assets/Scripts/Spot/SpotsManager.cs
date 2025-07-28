using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpotsManager : IDisposable
{
     public List<Spot> Spots => _spots;
     private List<Spot> _spots;
     private List<Task> _tasks = new List<Task>();
     private Spot _currentSpot;
     
     private BusEvents _busEvents;
     private LevelEndEvents _levelEndEvents;
     
     public SpotsManager(List<Spot> spots, LevelEndEvents levelEndEvents, BusEvents busEvents)
     { 
          _spots = spots;
          _levelEndEvents = levelEndEvents;
          _busEvents = busEvents;

          _busEvents.AddBusSpotTick(SeatActionsTask);
     }

     public void Dispose()
     {
          _busEvents.RemoveBusSpotTick(SeatActionsTask);
     }
     
     public Spot GetFirstEmptySpot(Transform transform,StickMan stickMan)
     {
          foreach (var spot in _spots)
          {
               if (spot.IsFull == false)
               {
                    spot.StickMan = stickMan;
                    spot.IsFull = true;
                    
                    return spot;
               }
          }
          
          //Level failed Actions
          _levelEndEvents.FireOnAllSeatsFullEnd();
          _currentSpot = new Spot((int)transform.position.x, (int)transform.position.z,false,transform);
          return _currentSpot; 
     }

     public bool IsAllSpotsFull()
     {
          foreach (var spot in _spots)
          {
               if (spot.IsFull == false)
                    return false;
          }
          return true;
     }
     
     private BusSeat GetSeat()
     {
          if (_busEvents.ActiveBus == null) return null;
          return _busEvents.ActiveBus.GetAnyEmptySeat();
     }
     
     private async void SeatActionsTask()
     {
          if (_busEvents.ActiveBus == null) return;
          int sameColorStickman = 0;
          _tasks.Clear();
          
          for (int i = 0; i < _spots.Count; i++)
          {
               if(_spots[i].StickMan == null) continue;
               if (_busEvents.ActiveBus.colorIndex != _spots[i].StickMan.ColorIndex) continue;
               if(sameColorStickman >= 3) break;
               
               var seat = GetSeat();
               _tasks.Add(_spots[i].StickMan.MoveToBus(seat,_busEvents.ActiveBus));
               if (seat != null)
               {
                    seat.IsFull = true;
                    seat.IsMarked = true;
                    _spots[i].IsFull = false;
                    _spots[i].StickMan = null;
                    
                    sameColorStickman++;
               }
          }
          
          await Task.WhenAll(_tasks);
          if(_busEvents.ActiveBus.IsFull()) _busEvents.FireBusFullTick();
     }
}