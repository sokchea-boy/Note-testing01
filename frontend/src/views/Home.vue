<template>
  <div class="container mx-auto px-4 py-8">
    <div class="flex justify-between items-center mb-8">
      <h1 class="text-3xl font-bold text-gray-900">Notes</h1>
      <router-link
        to="/note/new"
        class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
      >
        Create Note
      </router-link>
    </div>
    <div v-if="notesStore.loading" class="text-center">Loading...</div>
    <div v-else-if="notesStore.error" class="text-center text-red-600">{{ notesStore.error }}</div>
    <NoteList v-else />
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import NoteList from '@/components/NoteList.vue'
import { useNotesStore } from '@/stores/notesStore'

const notesStore = useNotesStore()

onMounted(() => {
  notesStore.fetchNotes()
})
</script>