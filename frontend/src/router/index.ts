import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/views/Home.vue'
import NoteDetailView from '@/views/NoteDetailView.vue'
import NoteEditor from '@/components/NoteEditor.vue'

const routes = [
  { path: '/', component: Home },
  { path: '/note/:id', component: NoteDetailView },
  { path: '/note/new', component: NoteEditor },
  { path: '/note/:id/edit', component: NoteEditor }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router