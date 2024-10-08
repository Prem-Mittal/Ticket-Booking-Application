import { CanActivateFn , Router , ActivatedRouteSnapshot , RouterStateSnapshot} from '@angular/router';
import { inject } from '@angular/core';
import { UsersService } from '../auth/services/users.service';
import { User } from '../auth/models/user.model';
export const authGuardGuard: CanActivateFn = (route:ActivatedRouteSnapshot, state:RouterStateSnapshot) => {
  const router:Router=inject(Router); 
  
  const userService = inject(UsersService); 
  const user: User | undefined = userService.getuser(); 
  const session: boolean = !user;

  const protectedRoutes: string[]=['/profile','/update-password/:id','/update-event/:id','/booking/:eventId/:price','/create-event'];
  return protectedRoutes.includes(state.url)&& session ? router.navigate(['/login']):true;
  
};
