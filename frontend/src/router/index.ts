import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/views/Home.vue'
import NoteDetailView from '@/views/NoteDetailView.vue'
import NoteEditor from '@/components/NoteEditor.vue'
import LoginForm from '@/components/LoginForm.vue'
import RegisterForm from '@/components/RegisterForm.vue'
import { useAuthStore } from '@/stores/authStore'

const routes = [
  { path: '/', component: Home, meta: { requiresAuth: true } },
  { path: '/note/:id', component: NoteDetailView, meta: { requiresAuth: true } },
  { path: '/note/new', component: NoteEditor, meta: { requiresAuth: true } },
  { path: '/note/:id/edit', component: NoteEditor, meta: { requiresAuth: true } },
  { path: '/login', component: LoginForm },
  { path: '/register', component: RegisterForm }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next('/login')
  } else if ((to.path === '/login' || to.path === '/register') && authStore.isAuthenticated) {
    next('/')
  } else {
    next()
  }
})

export default router