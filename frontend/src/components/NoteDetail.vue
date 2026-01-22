<template>
  <div class="max-w-2xl mx-auto">
    <div class="bg-white shadow-lg rounded-lg p-6">
      <h1 class="text-2xl font-bold text-gray-900 mb-4">{{ note.title }}</h1>
      <p class="text-gray-700 mb-4 whitespace-pre-wrap">{{ note.content }}</p>
      <div class="text-sm text-gray-500 mb-6">
        <p>Created: {{ formatDate(note.createdAt) }}</p>
        <p>Updated: {{ formatDate(note.updatedAt) }}</p>
      </div>
      <div class="flex space-x-4">
        <router-link
          :to="`/note/${note.id}/edit`"
          class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-indigo-600 bg-indigo-100 hover:bg-indigo-200"
        >
          Edit
        </router-link>
        <button
          @click="handleDelete"
          class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-red-600 bg-red-100 hover:bg-red-200"
        >
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import type { Note } from '@/types'
import { useNotesStore } from '@/stores/notesStore'

const props = defineProps<{
  note: Note
}>()

const router = useRouter()
const notesStore = useNotesStore()

async function handleDelete() {
  if (confirm('Are you sure you want to delete this note?')) {
    try {
      await notesStore.deleteNote(props.note.id)
      router.push('/')
    } catch (error) {
      alert('Failed to delete note')
    }
  }
}

function formatDate(dateString: string): string {
  return new Date(dateString).toLocaleString()
}
</script>